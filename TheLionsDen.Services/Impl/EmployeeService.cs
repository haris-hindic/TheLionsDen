using AutoMapper;
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
            if (!String.IsNullOrWhiteSpace(searchObject.JobType))
            {
                filteredQuery = filteredQuery.Where(x => x.JobType.Equals(searchObject.JobType));
            }
            if (!String.IsNullOrWhiteSpace(searchObject.Status))
            {
                filteredQuery = filteredQuery.Where(x => x.Status.Equals(searchObject.Status));
            }
            if (searchObject.FacilityId > 0)
            {
                filteredQuery = filteredQuery.Where(x => x.FacilityId == searchObject.FacilityId);
            }

            return filteredQuery;
        }

        public override EmployeeResponse GetById(int id)
        {
            var entity = context.Employees.Include("Facility").FirstOrDefault(x => x.EmployeeId == id);

            return mapper.Map<EmployeeResponse>(entity);
        }
    }
}
