using TheLionsDen.Model.Requests;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;

namespace TheLionsDen.Services
{
    public interface IRoomService : ICRUDService<RoomResponse,RoomSearchObject,RoomUpsertRequest, RoomUpsertRequest>
    {
        Task<string> Activate(int id);
        Task<string> Hide(int id);
        Task<string> SetAsTaken(int id);
        Task<bool> CheckRoomAvailability(int id, CheckAvailabilityRequest request);
        Task<List<string>> GetBookedDates(int id);
        Task<string> SaveRoom(int userId, int roomId);
        Task<string> RemoveSavedRoom(int userId, int roomId);
        Task<RoomResponse> RemoveAmenity(int roomId,int amenityId);
        Task<RoomResponse> GetWithSavedInd(int roomId,int userId);
    }
}
