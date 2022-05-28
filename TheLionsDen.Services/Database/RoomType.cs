using System;
using System.Collections.Generic;

namespace TheLionsDen.Services.Database
{
    public partial class RoomType
    {
        public RoomType()
        {
            RoomImages = new HashSet<RoomImage>();
            Rooms = new HashSet<Room>();
        }

        public int RoomTypeId { get; set; }
        public string? Name { get; set; }
        public int? Capacity { get; set; }
        public int? Size { get; set; }
        public string? Description { get; set; }
        public string? Rules { get; set; }

        public virtual ICollection<RoomImage> RoomImages { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
