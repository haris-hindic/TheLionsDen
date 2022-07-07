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
    public class RoomController : BaseCRUDController<RoomResponse, RoomSearchObject, RoomUpsertRequest, RoomUpsertRequest>
    {
        private readonly IRoomService service;

        public RoomController(IRoomService service) : base(service)
        {
            this.service = service;
        }

        [HttpPut("{id}/Activate")]
        public async Task<string> ActivateAsync(int id)
        {
            return await service.Activate(id);
        }

        [HttpPut("{id}/Hide")]
        public async Task<string> Hide(int id)
        {
            return await service.Hide(id);
        }

        [HttpPut("{id}/Taken")]
        public async Task<string> SetAsTaken(int id)
        {
            return await service.SetAsTaken(id);
        }

        [HttpDelete("{roomId}/remove/{amenityId}")]
        public async Task<RoomResponse> RemoveAmenity(int roomId, int amenityId)
        {
            return await service.RemoveAmenity(roomId,amenityId);
        }

        [AllowAnonymous]
        public override Task<IEnumerable<RoomResponse>> Get([FromQuery] RoomSearchObject searchObject)
        {
            return base.Get(searchObject);
        }
        [AllowAnonymous]
        public override Task<RoomResponse> GetById(int id)
        {
            return base.GetById(id);
        }
    }
}
