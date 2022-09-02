using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TheLionsDen.Model.Requests
{
    public class EmployeeInsertRequest
    {
        [Required(AllowEmptyStrings = false),MaxLength(40)]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false), MaxLength(40)]
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        [Required]
        public DateTime EmploymentDate { get; set; }
        [MaxLength(40)]
        public string Address { get; set; }
        [MaxLength(40),Phone]
        public string PhoneNumber { get; set; }
        [Required(AllowEmptyStrings = false), EmailAddress,MaxLength(40)]
        public string Email { get; set; }
        [MaxLength(40)]
        public string Gender { get; set; }
        [Required(AllowEmptyStrings = false), MaxLength(40)]
        public string Status { get; set; }
        [Required]
        public int JobTypeId { get; set; }

    }
}
