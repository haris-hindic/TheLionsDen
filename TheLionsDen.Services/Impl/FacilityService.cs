using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLionsDen.Model;
using TheLionsDen.Model.Requests;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;
using TheLionsDen.Services.Database;

namespace TheLionsDen.Services.Impl
{
    public class FacilityService : BaseCRUDService<FacilityResponse, Facility, FacilitySearchObject, FacilityUpsertRequest, FacilityUpsertRequest>, IFacilityService
    {
        public FacilityService(TheLionsDenContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override IQueryable<Facility> AddFilter(IQueryable<Facility> query, FacilitySearchObject searchObject = null)
        {
            var filteredQuery = base.AddFilter(query, searchObject);

            if (!String.IsNullOrEmpty(searchObject.Name))
            {
                filteredQuery = filteredQuery.Where(x => x.Name.ToLower().Contains(searchObject.Name));
            }
            if (!String.IsNullOrEmpty(searchObject.Status))
            {
                filteredQuery = filteredQuery.Where(x => x.Status.Equals(searchObject.Status));
            }

            return filteredQuery;
        }

        public override IQueryable<Facility> AddInclude(IQueryable<Facility> query, FacilitySearchObject searchObject = null)
        {
            var includedQuery = base.AddInclude(query, searchObject);

            if (searchObject.IncludeEmployees)
            {
                includedQuery = includedQuery.Include("Employees.JobType");
            }

            return includedQuery;
        }

        static object isLocked = new object();
        static MLContext mlContext = null;
        static ITransformer model = null;
        public async Task<List<FacilityResponse>> Recommend(int id)
        {
            prepareTrainedData();

            var finalResult = getTopPredictions(id);

            return mapper.Map<List<FacilityResponse>>(finalResult);
        }

        private void prepareTrainedData()
        {
            lock (isLocked)
            {
                if (mlContext == null)
                {
                    mlContext = new MLContext();

                    var tmpData = context.Reservations.Include("ReservationFacilities").ToList();

                    var data = new List<ProductEntry>();

                    foreach (var x in tmpData)
                    {
                        if (x.ReservationFacilities.Count > 1)
                        {
                            var distinctItemId = x.ReservationFacilities.Select(y => y.FacilityId).ToList();

                            distinctItemId.ForEach(y =>
                            {
                                var relatedItems = x.ReservationFacilities.Where(z => z.FacilityId != y);

                                foreach (var z in relatedItems)
                                {
                                    data.Add(new ProductEntry()
                                    {
                                        ProductID = (uint)y,
                                        CoPurchaseProductID = (uint)z.FacilityId,
                                    });
                                }
                            });
                        }
                    }

                    var disctintData = data.Select(y => new
                    {
                        ProductID = y.ProductID,
                        CoPurchaseProductID = y.CoPurchaseProductID,
                    }).Distinct();

                    var productEntries = new List<ProductEntry>();

                    foreach (var item in disctintData)
                    {
                        productEntries.Add(new ProductEntry()
                        {
                            ProductID = item.ProductID,
                            CoPurchaseProductID = item.CoPurchaseProductID,
                            Label = 0
                        });
                    }

                    var traindata = mlContext.Data.LoadFromEnumerable(productEntries);

                    MatrixFactorizationTrainer.Options options = new MatrixFactorizationTrainer.Options();
                    options.MatrixColumnIndexColumnName = nameof(ProductEntry.ProductID);
                    options.MatrixRowIndexColumnName = nameof(ProductEntry.CoPurchaseProductID);
                    options.LabelColumnName = "Label";
                    options.LossFunction = MatrixFactorizationTrainer.LossFunctionType.SquareLossOneClass;
                    options.Alpha = 0.01;
                    options.Lambda = 0.025;
                    options.NumberOfIterations = 100;
                    options.C = 0.00001;


                    var est = mlContext.Recommendation().Trainers.MatrixFactorization(options);

                    model = est.Fit(traindata);
                }

            }
        }
        private List<Facility> getTopPredictions(int id)
        {
            List<Facility> allFacilites = new List<Facility>();

            for (int i = 0; i < 1000; i++)
            {
                var tmp = context.Facilities
                .Where(x => x.FacilityId != id);

                allFacilites.AddRange(tmp);
            }


            var predictionResult = new List<Tuple<Facility, float>>();

            foreach (var item in allFacilites)
            {
                var predictionEngine = mlContext.Model.CreatePredictionEngine<ProductEntry, Copurchase_prediction>(model);
                var prediction = predictionEngine.Predict(new ProductEntry()
                {
                    ProductID = (uint)id,
                    CoPurchaseProductID = (uint)item.FacilityId
                });

                predictionResult.Add(new Tuple<Facility, float>(item, prediction.Score));
            }

            return predictionResult.OrderByDescending(x => x.Item2).Distinct()
                .Select(x => x.Item1).Take(3).ToList();
        }


        #region VALIDATIONS
        public override void validateUpdateRequest(int id, FacilityUpsertRequest request)
        {
            var errorMessage = new StringBuilder();

            validateFacilityExist(id, errorMessage);

            if (errorMessage.Length > 0)
                throw new UserException(errorMessage.ToString());
        }

        public override void validateGetByIdRequest(int id)
        {
            var errorMessage = new StringBuilder();

            validateFacilityExist(id, errorMessage);

            if (errorMessage.Length > 0)
                throw new UserException(errorMessage.ToString());
        }

        private void validateFacilityExist(int id, StringBuilder errorMessage)
        {
            var facility = context.Facilities.FirstOrDefault(x => x.FacilityId == id);
            if (facility == null)
                errorMessage.Append("You entered a non existent facility!\n");
        }
        #endregion
    }

    public class Copurchase_prediction
    {
        public float Score { get; set; }
    }

    public class ProductEntry
    {
        [KeyType(count: 500)]
        public uint ProductID { get; set; }

        [KeyType(count: 500)]
        public uint CoPurchaseProductID { get; set; }

        public float Label { get; set; }
    }
}
