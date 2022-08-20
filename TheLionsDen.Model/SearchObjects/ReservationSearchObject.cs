using System;
using System.Collections.Generic;
using System.Text;

namespace TheLionsDen.Model.SearchObjects
{
    public class ReservationSearchObject : BaseSearchObject
    {
        public DateTime Date { get; set; }
        public string Comparator { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }

        public bool IncludeUser { get; set; }
        public bool IncludeRoom { get; set; }
        public bool IncludePaymentDetails { get; set; }
        public bool IncludeFacilites { get; set; }
    }
}
