using System;
using System.Collections.Generic;

namespace TheLionsDen.Services.Database
{
    public partial class RoomImage
    {
        public int RoomImageId { get; set; }
        public string? Image { get; set; }
        public int? RoomTypeId { get; set; }

        public virtual RoomType? RoomType { get; set; }
    }
}
