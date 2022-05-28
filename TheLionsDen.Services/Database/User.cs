using System;
using System.Collections.Generic;

namespace TheLionsDen.Services.Database
{
    public partial class User
    {
        public User()
        {
            Favourites = new HashSet<Favourite>();
            Reservations = new HashSet<Reservation>();
            UserRoles = new HashSet<UserRole>();
        }

        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Status { get; set; }
        public string? PasswordSalt { get; set; }
        public string? PasswordHash { get; set; }

        public virtual ICollection<Favourite> Favourites { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
