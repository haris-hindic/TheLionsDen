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
        }

        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string Status { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Favourite> Favourites { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
