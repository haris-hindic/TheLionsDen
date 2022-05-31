using System;
using System.Collections.Generic;

namespace TheLionsDen.Services.Database
{
    public partial class PaymentDetail
    {
        public PaymentDetail()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int PaymentDetailsId { get; set; }
        public DateTime Date { get; set; }
        public string PaymentType { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string? CardNumber { get; set; }
        public string? ExpDate { get; set; }
        public int? Cvc { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
