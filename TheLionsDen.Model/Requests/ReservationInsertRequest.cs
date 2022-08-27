using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TheLionsDen.Model.Requests
{
    public class ReservationInsertRequest
    {
        [Required]
        public DateTime Arrival { get; set; }
        [Required]
        public DateTime Departure { get; set; }
        [Required]
        public float TotalPrice { get; set; }
        [MaxLength(40)]
        public string EstimatedArrivalTime { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int RoomId { get; set; }
        [Required]
        public PaymentDetailsInsertRequest PaymentDetails { get; set; }

        public List<int> FacilityIds { get; set; }
    }
}
