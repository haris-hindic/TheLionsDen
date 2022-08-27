using System;
using System.Collections.Generic;
using System.Text;

namespace TheLionsDen.Model.Responses
{
    public class ReservationFaciliteResponse
    {
        public int ReservaitonFacilityId { get; set; }
        public int ReservationId { get; set; }
        public int FacilityId { get; set; }

        public virtual FacilityResponse Facility { get; set; }
    }
}
