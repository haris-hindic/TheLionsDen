using System;
using System.Collections.Generic;

namespace TheLionsDen.Services.Database
{
    public partial class ReservationFacilite
    {
        public int ReservaitonFacilityId { get; set; }
        public int ReservationId { get; set; }
        public int FacilityId { get; set; }

        public virtual Facility Facility { get; set; } = null!;
        public virtual Reservation Reservation { get; set; } = null!;
    }
}
