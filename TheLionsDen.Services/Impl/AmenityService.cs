using AutoMapper;
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
    public class AmenityService : BaseCRUDService<AmenityResponse, Amenity, AmenitySearchObject, AmenityUpsertRequest, AmenityUpsertRequest>, IAmenityService
    {
        public AmenityService(TheLionsDenContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override IQueryable<Amenity> AddFilter(IQueryable<Amenity> query, AmenitySearchObject searchObject = null)
        {
            var filteredQuery = base.AddFilter(query, searchObject);

            if (!String.IsNullOrWhiteSpace(searchObject.Name))
            {
                filteredQuery = filteredQuery.Where(x => x.Name.ToLower().Contains(searchObject.Name.ToLower()));
            }
            if (searchObject.AmenityIds.Count > 0)
            {
                filteredQuery = filteredQuery.Where(x => searchObject.AmenityIds.Contains(x.AmenityId));
            }

            return filteredQuery;
        }

        #region VALIDATIONS
        public override void validateUpdateRequest(int id, AmenityUpsertRequest request)
        {
            var errorMessage = new StringBuilder();

            validateAmenityExist(id, errorMessage);

            if (errorMessage.Length > 0)
                throw new UserException(errorMessage.ToString());
        }

        public override void validateGetByIdRequest(int id)
        {
            var errorMessage = new StringBuilder();

            validateAmenityExist(id, errorMessage);

            if (errorMessage.Length > 0)
                throw new UserException(errorMessage.ToString());
        }

        private void validateAmenityExist(int id, StringBuilder errorMessage)
        {
            var amenity = context.Amenities.FirstOrDefault(x => x.AmenityId == id);
            if (amenity == null)
                errorMessage.Append("You entered a non existent amenity!\n");
        }
        #endregion
    }
}
