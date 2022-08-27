using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TheLionsDen.Model.Requests
{
    public class FacilityUpsertRequest
    {
        [Required(AllowEmptyStrings = false), MaxLength(40)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public float Price { get; set; }
        [Required(AllowEmptyStrings = false), MaxLength(40)]
        public string Status { get; set; }
        [Required]
        public byte[] Image { get; set; }
    }
}
