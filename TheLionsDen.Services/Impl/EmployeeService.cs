using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Text;
using TheLionsDen.Model;
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
            if (searchObject.AvaliableForFacilityId > 0)
            {
                filteredQuery = filteredQuery.Where(x => x.FacilityId != searchObject.FacilityId && x.FacilityId == null);
            }

            return filteredQuery;
        }

        public override IQueryable<Employee> AddInclude(IQueryable<Employee> query, EmployeeSearchObject searchObject = null)
        {
            var includedQuery = base.AddInclude(query, searchObject);

            if (searchObject.IncludeFacility)
            {
                includedQuery = includedQuery.Include("Facility");
            }
            if (searchObject.IncludeJobType)
            {
                includedQuery = includedQuery.Include("JobType");
            }

            return includedQuery;
        }

        public async Task<string> AssignToFacility(int employeeId, int facilityId)
        {
            validateAssignRequest(employeeId, facilityId);

            var entity = await context.Employees.FindAsync(employeeId);


            if (entity != null)
            {
                entity.FacilityId = facilityId;
                context.Update(entity);
                await context.SaveChangesAsync();
                return $"Successfully added employee, with id -> {employeeId}, to this facility!";
            }

            return $"There is no such employee or facility!";
        }       

        public override async Task<EmployeeResponse> GetById(int id)
        {
            validateGetByIdRequest(id);

            var entity = await context.Employees.Include("Facility").Include("JobType").FirstOrDefaultAsync(x => x.EmployeeId == id);

            return mapper.Map<EmployeeResponse>(entity);
        }

        public override async Task<EmployeeResponse> Insert(EmployeeInsertRequest request)
        {
            var entity = await base.Insert(request);

            context.SaveChanges();

            return await GetById(entity.EmployeeId);
        }

        public async Task<string> RemoveFromFacility(int id)
        {
            validateRemoveFromFacility(id);

            var entity = await context.Employees.FindAsync(id);


            if (entity != null)
            {
                entity.FacilityId = null;
                context.Update(entity);
                await context.SaveChangesAsync();
                return $"Successfully removed employee, with id -> {id}, from his facility!";
            }

            return $"There is no entity with id -> {id}";
        }

        public override async Task<EmployeeResponse> Update(int id, EmployeeUpdateRequest request)
        {
            await base.Update(id, request);

            return await GetById(id);
        }

        #region VALIDATIONS
        public override void validateInsertRequest(EmployeeInsertRequest request)
        {
            var errorMessage = new StringBuilder();

            validateJobTypeExist(request.JobTypeId, errorMessage);

            if (errorMessage.Length > 0)
                throw new UserException(errorMessage.ToString());
        }

        public override void validateUpdateRequest(int id, EmployeeUpdateRequest request)
        {
            var errorMessage = new StringBuilder();

            validateJobTypeExist(request.JobTypeId, errorMessage);
            validateEmployeeExist(id, errorMessage);

            if (errorMessage.Length > 0)
                throw new UserException(errorMessage.ToString());
        }

        public override void validateGetByIdRequest(int id)
        {
            var errorMessage = new StringBuilder();

            validateEmployeeExist(id, errorMessage);

            if (errorMessage.Length > 0)
                throw new UserException(errorMessage.ToString());
        }

        private void validateAssignRequest(int employeeId, int facilityId)
        {
            var errorMessage = new StringBuilder();

            validateEmployeeExist(employeeId, errorMessage);
            validateEmployeeIsAvaliable(employeeId, errorMessage);
            validateFacilityExist(facilityId, errorMessage);
            

            if (errorMessage.Length > 0)
                throw new UserException(errorMessage.ToString());
        }

        private void validateRemoveFromFacility(int id)
        {
            var errorMessage = new StringBuilder();

            validateEmployeeExist(id, errorMessage);
            validateEmployeeHasFacilityAssigned(id, errorMessage);

            if (errorMessage.Length > 0)
                throw new UserException(errorMessage.ToString());
        }

        private void validateEmployeeHasFacilityAssigned(int id, StringBuilder errorMessage)
        {
            var employee = context.Employees.FirstOrDefault(x => x.EmployeeId == id);
            if (employee != null && employee.FacilityId == null)
                errorMessage.Append("The employee you entered is not assigned to any facility!\n");
        }

        private void validateEmployeeIsAvaliable(int employeeId, StringBuilder errorMessage)
        {
            var employee = context.Employees.FirstOrDefault(x => x.EmployeeId == employeeId);
            if (employee != null && employee.FacilityId != null)
                errorMessage.Append("The employee you entered is not avaliable!\n");
        }

        private void validateEmployeeExist(int id, StringBuilder errorMessage)
        {
            var employee = context.Employees.FirstOrDefault(x => x.EmployeeId == id);
            if (employee == null)
                errorMessage.Append("You entered a non existent employee!\n");
        }

        private void validateFacilityExist(int id, StringBuilder errorMessage)
        {
            var facility = context.Facilities.FirstOrDefault(x => x.FacilityId == id);
            if (facility == null)
                errorMessage.Append("You entered a non existent facility!\n");
        }

        private void validateJobTypeExist(int jobTypeId, StringBuilder errorMessage)
        {
            var jobType = context.JobTypes.FirstOrDefault(x => x.JobTypeId == jobTypeId);
            if (jobType == null)
                errorMessage.Append("You entered a non existent room type!\n");
        }
        #endregion
    }
}
