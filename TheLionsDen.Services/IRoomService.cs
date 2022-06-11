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
    }
}
