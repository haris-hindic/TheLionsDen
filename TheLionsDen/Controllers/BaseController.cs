using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheLionsDen.Model.SearchObjects;
using TheLionsDen.Services;

namespace TheLionsDen.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class BaseController<T, TSearch> : ControllerBase where T : class where TSearch : BaseSearchObject
    {
        public IService<T, TSearch> service { get; set; }

        public BaseController(IService<T, TSearch> service)
        {
            this.service = service;
        }

        [HttpGet]
        public IEnumerable<T> Get([FromQuery] TSearch searchObject)
        {
            return service.Get(searchObject);
        }

        [HttpGet("{id}")]
        public T GetById(int id)
        {
            return service.GetById(id);
        }
    }
}
