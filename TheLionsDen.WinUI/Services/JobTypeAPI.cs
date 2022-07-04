using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;

namespace WinUI.Services
{
    public class JobTypeAPI : BaseAPIService<JobTypeResponse, BaseSearchObject>
    {
        public JobTypeAPI(string resourceName="jobtype") : base(resourceName)
        {
        }
    }
}
