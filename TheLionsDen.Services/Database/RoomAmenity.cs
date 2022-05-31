using System;
using System.Collections.Generic;

namespace TheLionsDen.Services.Database
{
    public partial class RoomAmenity
    {
        public int RoomAmenitiesId { get; set; }
        public int AmenityId { get; set; }
        public int RoomId { get; set; }

        public virtual Amenity Amenity { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;
    }
}
