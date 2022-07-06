using TheLionsDen.Model.Requests;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;

namespace TheLionsDen.WinUI.Services
{
    public class RoomImageAPI : CRUDAPIService<RoomImageResponse, RoomImageSearchObject, RoomImageInsertRequest, RoomImageInsertRequest>
    {
        public RoomImageAPI(string resourceName = "RoomImage") : base(resourceName)
        {
        }
    }
}
