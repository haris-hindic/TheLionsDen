using Microsoft.AspNetCore.Mvc;
using TheLionsDen.Model.Requests;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;
using TheLionsDen.Services;

namespace TheLionsDen.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoomImageController : BaseCRUDController<RoomImageResponse, RoomImageSearchObject, RoomImageInsertRequest, RoomImageInsertRequest>
    {
        public RoomImageController(IRoomImageService service) : base(service)
        {
        }
    }
}
