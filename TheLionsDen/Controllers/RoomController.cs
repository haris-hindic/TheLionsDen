using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheLionsDen.Auth;
using TheLionsDen.Model.Requests;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;
using TheLionsDen.Services;

namespace TheLionsDen.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoomController : BaseCRUDController<RoomResponse, RoomSearchObject, RoomUpsertRequest, RoomUpsertRequest>
    {
        private readonly IRoomService service;
        private readonly IUserService userService;

        public RoomController(IRoomService service,IUserService userService) : base(service)
        {
            this.service = service;
            this.userService = userService;
        }

        [Authorize(Roles = "Administrator,Employee")]
        public override Task<RoomResponse> Insert([FromBody] RoomUpsertRequest request)
        {
            return base.Insert(request);
        }

        [Authorize(Roles = "Administrator,Employee")]
        public override Task<RoomResponse> Update(int id, [FromBody] RoomUpsertRequest request)
        {
            return base.Update(id, request);
        }

        [Authorize(Roles = "Administrator,Employee")]
        public override Task<string> Delete(int id)
        {
            return base.Delete(id);
        }

        [Authorize(Roles = "Administrator,Employee")]
        [HttpPut("{id}/Activate")]
        public async Task<string> ActivateAsync(int id)
        {
            return await service.Activate(id);
        }

        [Authorize(Roles = "Administrator,Employee")]
        [HttpPut("{id}/Hide")]
        public async Task<string> Hide(int id)
        {
            return await service.Hide(id);
        }
        [Authorize(Roles = "Administrator,Employee")]
        [HttpPut("{id}/Taken")]
        public async Task<string> SetAsTaken(int id)
        {
            return await service.SetAsTaken(id);
        }
        [Authorize(Roles = "Administrator,Employee")]
        [HttpDelete("{roomId}/remove/{amenityId}")]
        public async Task<RoomResponse> RemoveAmenity(int roomId, int amenityId)
        {
            return await service.RemoveAmenity(roomId,amenityId);
        }

        [HttpPut("{userId}/save/{roomId}")]
        [Authorize(Roles = "Customer,Administrator")]
        public async Task<string> SaveRoom(int roomId, int userId)
        {
            return await service.SaveRoom(userId: userId, roomId: roomId);
        }

        [HttpDelete("{userId}/remove-save/{roomId}")]
        [Authorize(Roles = "Customer,Administrator")]
        public async Task<string> RemoveSavedRoom(int roomId, int userId)
        {
            return await service.RemoveSavedRoom(userId:userId, roomId: roomId);
        }

        [AllowAnonymous]
        public override Task<IEnumerable<RoomResponse>> Get([FromQuery] RoomSearchObject searchObject)
        {
            return base.Get(searchObject);
        }
        [AllowAnonymous]
        public async override Task<RoomResponse> GetById(int id)
        {
            var creds = CredentialsHelper.extractCredentials(Request);
            var user = await userService.Login(creds.Username,creds.Password);
            return await service.GetWithSavedInd(userId: user.UserId, roomId: id);
        }

        [HttpGet("{id}/check-availability"),AllowAnonymous]
        public async Task<bool> CheckRoomAvailability(int id, [FromQuery] CheckAvailabilityRequest request)
        {
            return await service.CheckRoomAvailability(id,request);
        }
    }
}
