using System;
using System.Collections.Generic;
using System.Text;

namespace TheLionsDen.Model.SearchObjects
{
    public class FacilitySearchObject : BaseSearchObject
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public bool IncludeEmployees { get; set; }
    }
}
