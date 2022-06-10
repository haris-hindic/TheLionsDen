using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TheLionsDen.Model.Responses
{
    public class RoomResponse
    {
        public int RoomId { get; set; }
        public string Name { get; set; } 
        public float Price { get; set; }
        public int? Number { get; set; }
        public int? Floor { get; set; }
        public string State { get; set; } 
        public int RoomTypeId { get; set; }

        public string Amenities { get; set; } = "No amenities assigned.";
        public string RoomTypeName { get; set; }
        [JsonIgnore]
        public virtual RoomTypeResponse RoomType { get; set; } 

        //public virtual ICollection<Favourite> Favourites { get; set; }
        //public virtual ICollection<Reservation> Reservations { get; set; }
        [JsonIgnore]
        public virtual ICollection<RoomAmenityResponse> RoomAmenities { get; set; }
    }
}
