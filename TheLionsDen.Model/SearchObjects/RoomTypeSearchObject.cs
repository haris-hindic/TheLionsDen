using System;
using System.Collections.Generic;
using System.Text;

namespace TheLionsDen.Model.SearchObjects
{
    public class RoomTypeSearchObject : BaseSearchObject
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Comparator { get; set; }
    }
}
