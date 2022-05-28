using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLionsDen.Model.SearchObjects;

namespace TheLionsDen.Services
{
    public interface ICRUDService<T,TSearch,TInsert,TUpdate> : IService<T,TSearch> where T : class
        where TSearch : BaseSearchObject where TInsert : class where TUpdate : class
    {
        T Insert(TInsert request);
        T Update(int id, TUpdate request);
        string Delete(int id);
    }
}
