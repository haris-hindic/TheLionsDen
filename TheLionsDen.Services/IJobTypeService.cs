using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;

namespace TheLionsDen.Services
{
    public interface IJobTypeService : IService<JobTypeResponse,BaseSearchObject>
    {
    }
}
