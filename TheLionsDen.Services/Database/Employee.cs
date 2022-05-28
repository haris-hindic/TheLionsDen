using System;
using System.Collections.Generic;

namespace TheLionsDen.Services.Database
{
    public partial class Employee
    {
        public int EmployeeId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? EmploymentDate { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? JobType { get; set; }
        public string? Status { get; set; }
        public int? FacilityId { get; set; }

        public virtual Facility? Facility { get; set; }
    }
}
