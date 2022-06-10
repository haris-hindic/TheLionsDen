using System;
using System.Collections.Generic;
using System.Text;

namespace TheLionsDen.Model.SearchObjects
{
    public class RoomSearchObject : BaseSearchObject
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public string Comparator { get; set; }
        public string State { get; set; }
        public int RoomTypeId { get; set; }

        public bool IncludeRoomType { get; set; }
        public bool IncludeAmenities { get; set; }
    }
}
