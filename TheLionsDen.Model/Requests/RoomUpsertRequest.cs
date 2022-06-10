using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TheLionsDen.Model.Requests
{
    public class RoomUpsertRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        [Required,Range(1,99999)]
        public float Price { get; set; }
        public int? Number { get; set; }
        public int? Floor { get; set; }
        [Required]
        public int RoomTypeId { get; set; }

        public List<int> AmenityIds { get; set; }
    }
}
