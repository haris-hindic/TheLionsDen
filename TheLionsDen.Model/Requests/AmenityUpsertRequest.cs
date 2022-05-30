using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TheLionsDen.Model.Requests
{
    public class AmenityUpsertRequest
    {
        [Required(AllowEmptyStrings = false), MinLength(2)]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
