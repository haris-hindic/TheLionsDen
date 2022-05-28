using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLionsDen.Model.Requests;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;
using TheLionsDen.Services.Database;

namespace TheLionsDen.Services
{
    public interface IAmenityService : ICRUDService<AmenityResponse,AmenitySearchObject,AmenityUpsertRequest, AmenityUpsertRequest> 
    {
    }
}
