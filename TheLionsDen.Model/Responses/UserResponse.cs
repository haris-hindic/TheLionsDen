using System;
using System.Collections.Generic;
using System.Text;

namespace TheLionsDen.Model.Responses
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; } 
        public string Email { get; set; } 
        public string PhoneNumber { get; set; }
        public string Status { get; set; } 
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public int RoleId { get; set; }

        public virtual RoleResponse Role { get; set; }
        //public virtual ICollection<Favourite> Favourites { get; set; }
        //public virtual ICollection<Reservation> Reservations { get; set; }

        public string FullName => $"{FirstName} {LastName}";
        public string RoleName => Role.Name;
    }
}
