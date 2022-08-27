using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TheLionsDen.Model.Requests
{
    public class UserInsertRequest
    {
        [Required(AllowEmptyStrings = false), MaxLength(40)]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false), MaxLength(40)]
        public string LastName { get; set; }
        [Required(AllowEmptyStrings = false), EmailAddress, MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(40)]
        public string PhoneNumber { get; set; }
        [Required(AllowEmptyStrings = false), MinLength(4), MaxLength(50)]
        public string Username { get; set; }
        [Required(AllowEmptyStrings = false), MaxLength(100)]
        public string Password { get; set; }
        [Required(AllowEmptyStrings = false), MaxLength(100)]
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
