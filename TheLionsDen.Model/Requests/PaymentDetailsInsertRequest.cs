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
        [Required]
        public string PaymentType { get; set; }
        public string CardNumber { get; set; }
        public string ExpDate { get; set; }
        public int Cvc { get; set; }
    }
}
