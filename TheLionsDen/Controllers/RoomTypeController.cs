﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheLionsDen.Model.Requests;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;
using TheLionsDen.Services;

namespace TheLionsDen.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoomTypeController : BaseCRUDController<RoomTypeResponse, RoomTypeSearchObject, RoomTypeUpsertRequest, RoomTypeUpsertRequest>
    {
        public RoomTypeController(IRoomTypeService service) : base(service)
        {
        }
    }
}
