using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TheLionsDen.Model.Requests
{
    public class UserUpdateRequest
    {
        [Required(AllowEmptyStrings = false), MaxLength(40)]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false), MaxLength(40)]
        public string LastName { get; set; }
        [Required(AllowEmptyStrings = false), EmailAddress, MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(40),Phone]
        public string PhoneNumber { get; set; }
        [MaxLength(100)]
        public string Password { get; set; }
        [MaxLength(100)]
        public string PasswordConfirmation { get; set; }
        [MaxLength(40)]
        public string Status { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [MaxLength(40)]
        public string Gender { get; set; }
        [Required]
        public int RoleId { get; set; }
    }
}
