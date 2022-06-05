using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
