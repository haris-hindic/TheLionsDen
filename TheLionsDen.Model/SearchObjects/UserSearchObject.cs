using System;
using System.Collections.Generic;
using System.Text;

namespace TheLionsDen.Model.SearchObjects
{
    public class UserSearchObject : BaseSearchObject
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public bool IncludeRoles { get; set; }
    }
}
