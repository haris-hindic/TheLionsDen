using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
}
