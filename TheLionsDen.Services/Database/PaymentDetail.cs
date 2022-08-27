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
        public string? Currency { get; set; }
        public string? StripeId { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
