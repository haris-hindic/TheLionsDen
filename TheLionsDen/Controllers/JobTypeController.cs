using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;
using TheLionsDen.Services;

namespace TheLionsDen.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JobTypeController : BaseController<JobTypeResponse, BaseSearchObject>
    {
        public JobTypeController(IJobTypeService service) : base(service)
        {
        }
    }
}
