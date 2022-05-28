using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheLionsDen.Model.SearchObjects;
using TheLionsDen.Services;

namespace TheLionsDen.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BaseCRUDController<T, TSearch, TInsert, TUpdate> : BaseController<T, TSearch>
         where T : class where TSearch : BaseSearchObject where TInsert : class where TUpdate : class
    {
        public BaseCRUDController(ICRUDService<T, TSearch, TInsert, TUpdate> service) : base(service)
        {

        }

        [HttpPost]
        public virtual T Insert([FromBody] TInsert request)
        {
            return ((ICRUDService<T, TSearch, TInsert, TUpdate>)this.service).Insert(request);
        }

        [HttpPut("{id}")]
        public virtual T Update(int id, [FromBody] TUpdate request)
        {
            return ((ICRUDService<T, TSearch, TInsert, TUpdate>)this.service).Update(id, request);
        }

        [HttpDelete("{id}")]
        public virtual string Delete(int id)
        {
            return ((ICRUDService<T, TSearch, TInsert, TUpdate>)this.service).Delete(id);
        }
    }
}
