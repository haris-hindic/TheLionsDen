using System;
using System.Collections.Generic;

namespace TheLionsDen.Services.Database
{
    public partial class Favourite
    {
        public int FavouriteId { get; set; }
        public DateTime Added { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }

        public virtual Room Room { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
