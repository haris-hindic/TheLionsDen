using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TheLionsDen.Model.Requests
{
    public class RoomImageInsertRequest
    {
        [Required]
        public byte[] Image { get; set; }
        [Required]
        public int RoomTypeId { get; set; }
    }
}
