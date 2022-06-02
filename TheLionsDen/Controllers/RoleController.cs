using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;
using TheLionsDen.Services;

namespace TheLionsDen.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoleController : BaseController<RoleResponse, BaseSearchObject>
    {
        public RoleController(IRoleService service) : base(service)
        {
        }
    }
}
