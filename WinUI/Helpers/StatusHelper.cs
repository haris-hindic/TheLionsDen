using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinUI.Helpers
{
    public static class StatusHelper
    {
        public static List<string> employee = new List<string>()
        {
            "Active","Inactive","Vacation","Recovery"
        };
        public static List<string> facility = new List<string>()
        {
            "Active","Closed","Renovation"
        };
    }
}
