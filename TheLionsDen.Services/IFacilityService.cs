using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLionsDen.Model.Requests;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;

namespace TheLionsDen.Services
{
    public interface IFacilityService : ICRUDService<FacilityResponse,FacilitySearchObject,FacilityUpsertRequest, FacilityUpsertRequest>
    {
        Task<List<FacilityResponse>> Recommend(int id);
    }
}
