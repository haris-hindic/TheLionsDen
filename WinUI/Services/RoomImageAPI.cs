using TheLionsDen.Model.Requests;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;

namespace WinUI.Services
{
    public class RoomImageAPI : CRUDAPIService<RoomImageResponse, RoomImageSearchObject, RoomImageInsertRequest, RoomImageInsertRequest>
    {
        public RoomImageAPI(string resourceName = "RoomImage") : base(resourceName)
        {
        }
    }
}
