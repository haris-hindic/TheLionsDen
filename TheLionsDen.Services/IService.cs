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
        Task<IEnumerable<T>> Get(TSearch searchObject);
        Task<T> GetById(int id);
    }
}
