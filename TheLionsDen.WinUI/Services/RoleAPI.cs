using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;

namespace TheLionsDen.WinUI.Services
{
    public class RoleAPI : BaseAPIService<RoleResponse, BaseSearchObject>
    {
        public RoleAPI(string resourceName = "role") : base(resourceName)
        {
        }
    }
}
