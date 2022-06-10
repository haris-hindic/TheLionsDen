using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLionsDen.Model.SearchObjects;
using TheLionsDen.Services.Database;

namespace TheLionsDen.Services.Impl
{
    public class BaseService<T, TDb, TSearch> : IService<T, TSearch> where T : class where TDb : class where TSearch : BaseSearchObject
    {
        public readonly TheLionsDenContext context;
        private readonly IMapper mapper;
        private readonly DbSet<TDb> dbSet;

        public BaseService(TheLionsDenContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.dbSet = context.Set<TDb>();
        }

        public virtual async Task<IEnumerable<T>> Get(TSearch searchObject = null)
        {
            var entityQuery = dbSet.AsQueryable();

            entityQuery = AddFilter(entityQuery, searchObject);
            entityQuery = AddInclude(entityQuery, searchObject);

            if (searchObject.Page.HasValue && searchObject.RecordsPerPage.HasValue)
            {
                entityQuery = entityQuery.Skip(searchObject.Page.Value * searchObject.RecordsPerPage.Value)
                    .Take(searchObject.RecordsPerPage.Value);
            }

            var list = await entityQuery.ToListAsync();

            return mapper.Map<List<T>>(list);
        }

        public virtual IQueryable<TDb> AddFilter(IQueryable<TDb> query, TSearch searchObject = null)
        {
            return query;
        }

        public virtual IQueryable<TDb> AddInclude(IQueryable<TDb> query, TSearch searchObject = null)
        {
            return query;
        }

        public virtual async Task<T> GetById(int id)
        {
            validateGetByIdRequest(id);

            var entity = await dbSet.FindAsync(id);

            return mapper.Map<T>(entity);
        }


        public virtual void validateGetByIdRequest(int id)
        {

        }
    }
}
