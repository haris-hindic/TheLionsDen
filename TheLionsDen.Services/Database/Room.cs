using System;
using System.Collections.Generic;

namespace TheLionsDen.Services.Database
{
    public partial class Room
    {
        public Room()
        {
            Favourites = new HashSet<Favourite>();
            Reservations = new HashSet<Reservation>();
            RoomAmenities = new HashSet<RoomAmenity>();
        }

        public int RoomId { get; set; }
        public string? Name { get; set; }
        public float? Price { get; set; }
        public int? Number { get; set; }
        public int? Floor { get; set; }
        public string? State { get; set; }
        public int? RoomTypeId { get; set; }

        public virtual RoomType? RoomType { get; set; }
        public virtual ICollection<Favourite> Favourites { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<RoomAmenity> RoomAmenities { get; set; }
    }
}
