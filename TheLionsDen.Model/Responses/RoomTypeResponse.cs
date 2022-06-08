using System;
using System.Collections.Generic;
using System.Text;

namespace TheLionsDen.Model.Responses
{
    public class RoomTypeResponse
    {
        public int RoomTypeId { get; set; }
        public string Name { get; set; } 
        public int Capacity { get; set; }
        public int Size { get; set; }
        public string Description { get; set; }
        public string Rules { get; set; }

        public string SizeM2 => $"{Size}m2";

        public virtual ICollection<RoomImageResponse> RoomImages { get; set; }
        //public virtual ICollection<Room> Rooms { get; set; }
    }
}
