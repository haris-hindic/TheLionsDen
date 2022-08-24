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
    public class FacilityController : BaseCRUDController<FacilityResponse, FacilitySearchObject, FacilityUpsertRequest, FacilityUpsertRequest>
    {
        private readonly IFacilityService service;

        public FacilityController(IFacilityService service) : base(service)
        {
            this.service = service;
        }

        [NonAction]
        public override Task<string> Delete(int id)
        {
            return base.Delete(id);
        }

        [HttpGet("reccommend/{id}"),Authorize]
        public async Task<List<FacilityResponse>> Reccommend(int id)
        {
            return await service.Recommend(id);
        }
    }
}
