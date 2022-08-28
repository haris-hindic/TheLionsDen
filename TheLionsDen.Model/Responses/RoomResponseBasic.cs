using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TheLionsDen.Model.Responses
{
    public class RoomResponseBasic
    {
        public int RoomId { get; set; }
        public string Name { get; set; } 
        public float Price { get; set; }
        public string State { get; set; } 
        public int RoomTypeId { get; set; }

        public string Amenities { get; set; } = "No amenities assigned.";
        public string RoomTypeName { get; set; }

        public bool isSaved { get; set; }

        public byte[] CoverImage { get; set; }
    }
}
