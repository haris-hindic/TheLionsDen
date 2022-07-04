using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinUI.Helpers
{
    public static class cmbHelper
    {
        public static List<string> employee = new List<string>()
        {
            "Active","Inactive","Vacation","Recovery"
        };
        public static List<string> facility = new List<string>()
        {
            "Active","Closed","Renovation"
        };
        public static List<string> comparator = new List<string>()
        {
            "<",">","=",">=","<="
        };
        public static List<string> roomState = new List<string>()
        {
            "New","Active","Hidden","Taken","Unavaliable"
        };
    }
}
