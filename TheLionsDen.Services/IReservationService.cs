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
    public interface IReservationService : ICRUDService<ReservationResponse,ReservationSearchObject,ReservationInsertRequest,object>
    {
        Task<string> Confirm(int id);
        Task<string> Cancel(int id);
    }
}
