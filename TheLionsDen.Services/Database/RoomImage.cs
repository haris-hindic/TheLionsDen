using System;
using System.Collections.Generic;

namespace TheLionsDen.Services.Database
{
    public partial class RoomImage
    {
        public int RoomImageId { get; set; }
        public byte[] Image { get; set; } = null!;
        public int RoomTypeId { get; set; }

        public virtual RoomType RoomType { get; set; } = null!;
    }
}
