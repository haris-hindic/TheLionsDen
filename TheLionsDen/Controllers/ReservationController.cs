using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheLionsDen.Model.Requests;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;
using TheLionsDen.Services;

namespace TheLionsDen.Controllers
{
    public class ReservationController : BaseCRUDController<ReservationResponse, ReservationSearchObject, ReservationInsertRequest, object>
    {
        private readonly IReservationService service;

        public ReservationController(IReservationService service) : base(service)
        {
            this.service = service;
        }

        [NonAction]
        public override Task<ReservationResponse> Update(int id, [FromBody] object request)
        {
            return base.Update(id, request);
        }


        [HttpPut("{id}/Cancel")]
        public async Task<string> Cancel(int id)
        {
            return await service.Cancel(id);
        }

        [HttpPut("{id}/Confirm")]
        public async Task<string> Confirm(int id)
        {
            return await service.Confirm(id);
        }

    }
}
