using System;
using System.Collections.Generic;
using System.Text;

namespace TheLionsDen.Model.Responses
{
    public class RoomAmenityResponse
    {
        public int RoomAmenitiesId { get; set; }
        public int AmenityId { get; set; }
        public int RoomId { get; set; }

        public string AmenityName =>Amenity.Name;

        public virtual AmenityResponse Amenity { get; set; }
    }
}
