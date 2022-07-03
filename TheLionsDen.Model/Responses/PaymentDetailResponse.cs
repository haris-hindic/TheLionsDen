using System;
using System.Collections.Generic;
using System.Text;

namespace TheLionsDen.Model.Responses
{
    public class PaymentDetailResponse
    {
        public int PaymentDetailsId { get; set; }
        public DateTime Date { get; set; }
        public string PaymentType { get; set; } 
        public string CardNumber { get; set; }
        public string ExpDate { get; set; }
        public int Cvc { get; set; }
    }
}
