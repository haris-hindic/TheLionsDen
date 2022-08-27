using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TheLionsDen.Model.Requests
{
    public class PaymentDetailsInsertRequest
    {
        [Required]
        public DateTime Date { get; set; }
        [Required, MaxLength(40)]
        public string PaymentType { get; set; }
        [MaxLength(20)]
        public string Currency { get; set; }
        [MaxLength(40)]
        public string StripeId { get; set; }
    }
}
