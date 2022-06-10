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
    public class RoomAPI : CRUDAPIService<RoomResponse, RoomSearchObject, RoomUpsertRequest, RoomUpsertRequest>
    {
        public RoomAPI(string resourceName="room") : base(resourceName)
        {
        }
    }
}
