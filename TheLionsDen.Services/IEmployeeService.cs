using TheLionsDen.Model.Requests;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;

namespace TheLionsDen.Services
{
    public interface IEmployeeService : ICRUDService<EmployeeResponse, EmployeeSearchObject, EmployeeInsertRequest, EmployeeUpdateRequest>
    {
        public Task<string> RemoveFromFacility(int id);
        public Task<string> AddToFromFacility(int employeeId,int facilityId);
    }

}
