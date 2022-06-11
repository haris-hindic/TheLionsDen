using System;
using System.Collections.Generic;
using System.Text;

namespace TheLionsDen.Model.SearchObjects
{
    public class AmenitySearchObject : BaseSearchObject
    {
        public string Name { get; set; }
        public List<int> AmenityIds { get; set; } = new List<int>();
        public int NotRoomId { get; set; }
    }
}
