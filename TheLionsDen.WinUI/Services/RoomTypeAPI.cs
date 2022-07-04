using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLionsDen.Model.Requests;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;

namespace WinUI.Services
{
    public class RoomTypeAPI : CRUDAPIService<RoomTypeResponse, RoomTypeSearchObject, RoomTypeUpsertRequest, RoomTypeUpsertRequest>
    {
        public RoomTypeAPI(string resourceName="RoomType") : base(resourceName)
        {
        }
    }
}
