using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TheLionsDen.Model.Requests
{
    public class EmployeeInsertRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false)]

        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        [Required]
        public DateTime EmploymentDate { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        [Required(AllowEmptyStrings = false), EmailAddress]
        public string Email { get; set; }
        public string Gender { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string JobType { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Status { get; set; }
    }
}
