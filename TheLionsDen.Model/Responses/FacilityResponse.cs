using System;
using System.Collections.Generic;
using System.Text;

namespace TheLionsDen.Model.Responses
{
    public class FacilityResponse
    {
        public int FacilityId { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; }
        public float Price { get; set; }
        public string Status { get; set; }
        public byte[] Image { get; set; }

        public string PriceCurrency => $"{Price}$";

        public virtual ICollection<EmployeeResponse> Employees { get; set; }
    }
}
