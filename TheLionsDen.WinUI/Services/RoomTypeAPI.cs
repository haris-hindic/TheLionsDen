using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLionsDen.Model.Requests;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;

namespace TheLionsDen.WinUI.Services
{
    public class RoomTypeAPI : CRUDAPIService<RoomTypeResponse, RoomTypeSearchObject, RoomTypeUpsertRequest, RoomTypeUpsertRequest>
    {
        public RoomTypeAPI(string resourceName = "RoomType") : base(resourceName)
        {
        }
    }
}
