using System;
using System.Collections.Generic;

namespace TheLionsDen.Services.Database
{
    public partial class Reservation
    {
        public Reservation()
        {
            ReservationFacilites = new HashSet<ReservationFacilite>();
        }

        public int ReservationId { get; set; }
        public DateTime Arrival { get; set; }
        public DateTime Departure { get; set; }
        public float TotalPrice { get; set; }
        public string? Status { get; set; }
        public int? UserId { get; set; }
        public int? RoomId { get; set; }

        public virtual Room? Room { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<ReservationFacilite> ReservationFacilites { get; set; }
    }
}
