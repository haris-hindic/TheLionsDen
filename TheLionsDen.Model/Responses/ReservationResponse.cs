using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TheLionsDen.Model.Responses
{
    public class ReservationResponse
    {
        public int ReservationId { get; set; }
        public DateTime Arrival { get; set; }
        public DateTime Departure { get; set; }
        public float TotalPrice { get; set; }
        public string Status { get; set; }
        public string SpecialRequests { get; set; }
        public string EstimatedArrivalTime { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public int PaymentDetailsId { get; set; }

        public string RoomName { get; set; }
        public string UserFullName { get; set; }
        public string FacilityNames { get; set; }

        public virtual PaymentDetailResponse PaymentDetails { get; set; }
        public virtual RoomResponse Room { get; set; }
        public virtual UserResponse User { get; set; }
        [JsonIgnore]
        public virtual ICollection<ReservationFaciliteResponse> ReservationFacilites { get; set; }
    }
}
