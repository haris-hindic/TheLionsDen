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
    public class AmenityController : BaseCRUDController<AmenityResponse, AmenitySearchObject, AmenityUpsertRequest, AmenityUpsertRequest>
    {
        public AmenityController(IAmenityService service) : base(service)
        {
        }
    }
}
