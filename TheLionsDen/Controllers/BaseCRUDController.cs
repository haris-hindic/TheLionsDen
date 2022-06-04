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
        public virtual async Task<T> Insert([FromBody] TInsert request)
        {
            return await ((ICRUDService<T, TSearch, TInsert, TUpdate>)this.service).Insert(request);
        }

        [HttpPut("{id}")]
        public virtual async Task<T> Update(int id, [FromBody] TUpdate request)
        {
            return await ((ICRUDService<T, TSearch, TInsert, TUpdate>)this.service).Update(id, request);
        }

        [HttpDelete("{id}")]
        public virtual async Task<string> Delete(int id)
        {
            return await ((ICRUDService<T, TSearch, TInsert, TUpdate>)this.service).Delete(id);
        }
    }
}
