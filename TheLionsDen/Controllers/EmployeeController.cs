using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheLionsDen.Model.Requests;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;
using TheLionsDen.Services;

namespace TheLionsDen.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator,Employee")]
    public class EmployeeController : BaseCRUDController<EmployeeResponse, EmployeeSearchObject, EmployeeInsertRequest, EmployeeUpdateRequest>
    {
        private readonly IEmployeeService service;

        public EmployeeController(IEmployeeService service) : base(service)
        {
            this.service = service;
        }

        [HttpGet("{employeeId}/assign/{facilityId}")]
        public Task<string> AssignToFacility([FromRoute] int employeeId, [FromRoute] int facilityId)
        {
            return service.AssignToFacility(employeeId, facilityId);
        }

        [HttpDelete("{employeeId}/remove-facility")]
        public Task<string> RemoveFromFacility([FromRoute] int employeeId)
        {
            return service.RemoveFromFacility(employeeId);
        }
    }
}
