using System;
using System.Collections.Generic;

namespace TheLionsDen.Services.Database
{
    public partial class Reservation
    {
        public Reservation()
        {
            ReservationFacilities = new HashSet<ReservationFacilities>();
        }

        public int ReservationId { get; set; }
        public DateTime Arrival { get; set; }
        public DateTime Departure { get; set; }
        public float TotalPrice { get; set; }
        public string Status { get; set; } = null!;
        public string? EstimatedArrivalTime { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public int PaymentDetailsId { get; set; }

        public virtual PaymentDetail PaymentDetails { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<ReservationFacilities> ReservationFacilities { get; set; }
    }
}
