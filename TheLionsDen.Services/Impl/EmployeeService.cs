﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TheLionsDen.Model.Requests;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;
using TheLionsDen.Services.Database;

namespace TheLionsDen.Services.Impl
{
    public class EmployeeService : BaseCRUDService<EmployeeResponse, Employee, EmployeeSearchObject, EmployeeInsertRequest, EmployeeUpdateRequest>, IEmployeeService
    {
        public EmployeeService(TheLionsDenContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override IQueryable<Employee> AddFilter(IQueryable<Employee> query, EmployeeSearchObject searchObject = null)
        {
            var filteredQuery = base.AddFilter(query, searchObject);

            if (!String.IsNullOrWhiteSpace(searchObject.Name))
            {
                filteredQuery = filteredQuery.Where(x => x.FirstName.ToLower().Contains(searchObject.Name.ToLower()) ||
                                                       x.LastName.ToLower().Contains(searchObject.Name.ToLower()));
            }
            if (searchObject.JobTypeId > 0)
            {
                filteredQuery = filteredQuery.Where(x => x.JobTypeId == searchObject.JobTypeId);
            }
            if (searchObject.FacilityId > 0)
            {
                filteredQuery = filteredQuery.Where(x => x.FacilityId == searchObject.FacilityId);
            }

            return filteredQuery;
        }

        public override IQueryable<Employee> AddInclude(IQueryable<Employee> query, EmployeeSearchObject searchObject = null)
        {
            var includedQuery = base.AddInclude(query, searchObject);

            //if (searchObject.IncludeFacility)
            //{
            //    includedQuery = includedQuery.Include("Facility");
            //}
            if (searchObject.IncludeJobType)
            {
                includedQuery = includedQuery.Include("JobType");
            }

            return includedQuery;
        }

        public override async Task<EmployeeResponse> GetById(int id)
        {
            var entity = await context.Employees.Include("Facility").Include("JobType").FirstOrDefaultAsync(x => x.EmployeeId == id);

            return mapper.Map<EmployeeResponse>(entity);
        }

        public override async Task<EmployeeResponse> Insert(EmployeeInsertRequest request)
        {
            var entity = await base.Insert(request);

            context.SaveChanges();

            return await GetById(entity.EmployeeId);
        }

        public override async Task<EmployeeResponse> Update(int id, EmployeeUpdateRequest request)
        {
            await base.Update(id, request);

            return await GetById(id);
        }
    }
}