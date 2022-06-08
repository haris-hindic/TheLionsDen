using System;
using System.Collections.Generic;
using System.Text;

namespace TheLionsDen.Model.Responses
{
    public class RoomImageResponse
    {
        public int RoomImageId { get; set; }
        public byte[] Image { get; set; } 
        public int RoomTypeId { get; set; }
    }
}
