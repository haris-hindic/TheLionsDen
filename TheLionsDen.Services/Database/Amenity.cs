using System;
using System.Collections.Generic;

namespace TheLionsDen.Services.Database
{
    public partial class Amenity
    {
        public Amenity()
        {
            RoomAmenities = new HashSet<RoomAmenity>();
        }

        public int AmenityId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<RoomAmenity> RoomAmenities { get; set; }
    }
}
