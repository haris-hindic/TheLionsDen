using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TheLionsDen.Model.Responses
{
    public class EmployeeResponse
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime EmploymentDate { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }
        public int FacilityId { get; set; }
        public int JobTypeId { get; set; }


        public string FullName => $"{FirstName} {LastName}";
        public string FacilityName { get; set; } = "No facility assigned."; //=> Facility == null ? "Not assigned" : $"{Facility.Name}";
        public string JobName { get; set; } // => $"{JobType.Name}";
        public string OutlineText { get; set; } // => $"{FullName} ({JobName})";

        [JsonIgnore]
        public virtual FacilityResponse Facility { get; set; }
        //[JsonIgnore]
        public virtual JobTypeResponse JobType { get; set; }

    }
}
