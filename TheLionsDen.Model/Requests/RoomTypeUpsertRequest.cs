using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TheLionsDen.Model.Requests
{
    public class RoomTypeUpsertRequest
    {
        [Required(AllowEmptyStrings = false),MaxLength(40)]
        public string Name { get; set; }
        [Required, Range(1, 100)]
        public int Capacity { get; set; }
        [Range(1,500)]
        public int Size { get; set; }
        public string Description { get; set; }
        public string Rules { get; set; }
    }
}
