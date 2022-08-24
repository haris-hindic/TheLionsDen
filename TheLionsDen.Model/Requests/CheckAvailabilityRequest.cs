using System;
using System.Collections.Generic;
using System.Text;

namespace TheLionsDen.Model.Requests
{
    public class CheckAvailabilityRequest
    {
        public DateTime Arrival { get; set; }
        public DateTime Departure { get; set; }
    }
}
