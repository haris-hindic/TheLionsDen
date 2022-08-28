using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheLionsDen.Model.Requests;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;
using TheLionsDen.Services;

namespace TheLionsDen.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FacilityController : BaseCRUDController<FacilityResponse, FacilitySearchObject, FacilityUpsertRequest, FacilityUpsertRequest>
    {
        private readonly IFacilityService service;

        public FacilityController(IFacilityService service) : base(service)
        {
            this.service = service;
        }

        [Authorize(Roles = "Administrator,Employee")]
        public override Task<FacilityResponse> Insert([FromBody] FacilityUpsertRequest request)
        {
            return base.Insert(request);
        }

        [Authorize(Roles = "Administrator,Employee")]
        public override Task<FacilityResponse> Update(int id, [FromBody] FacilityUpsertRequest request)
        {
            return base.Update(id, request);
        }
        [Authorize(Roles = "Administrator,Employee")]
        public override Task<FacilityResponse> GetById(int id)
        {
            return base.GetById(id);
        }

        [NonAction]
        public override Task<string> Delete(int id)
        {
            return base.Delete(id);
        }
        [AllowAnonymous]
        public override Task<IEnumerable<FacilityResponse>> Get([FromQuery] FacilitySearchObject searchObject)
        {
            return base.Get(searchObject);
        }

        [HttpGet("reccommend/{id}"), Authorize(Roles = "Customer,Administrator")]
        public async Task<List<FacilityResponse>> Reccommend(int id)
        {
            return await service.Recommend(id);
        }
    }
}
