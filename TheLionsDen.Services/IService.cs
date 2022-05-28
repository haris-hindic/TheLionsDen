using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLionsDen.Model.SearchObjects;

namespace TheLionsDen.Services
{
    public interface IService<T, TSearch> where T : class where TSearch : BaseSearchObject
    {
        IEnumerable<T> Get(TSearch searchObject);
        T GetById(int id);
    }
}
