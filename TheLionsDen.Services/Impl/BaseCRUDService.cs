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
    public class BaseCRUDService<T, TDb, TSearch, TInsert, TUpdate> : BaseService<T, TDb, TSearch>, ICRUDService<T, TSearch, TInsert, TUpdate>
        where T : class
        where TDb : class
        where TSearch : BaseSearchObject
        where TInsert : class
        where TUpdate : class
    {
        public DbSet<TDb> dbSet { get; set; }
        public readonly TheLionsDenContext context;
        public readonly IMapper mapper;
        public BaseCRUDService(TheLionsDenContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
            dbSet = context.Set<TDb>();
            this.mapper = mapper;
        }

        public string Delete(int id)
        {
            TDb entity = dbSet.Find(id);

            if (entity != null)
            {
                dbSet.Remove(entity);
                context.SaveChanges();
                return $"Successfully deleted entity with id -> {id}";
            }

            return $"There is no entity with id -> {id}";
        }

        public virtual T Insert(TInsert request)
        {
            var entity = mapper.Map<TDb>(request);

            dbSet.Add(entity);

            BeforeInsert(request, entity);

            context.SaveChanges();

            return mapper.Map<T>(entity);
        }

        public virtual void BeforeInsert(TInsert request, TDb entity)
        {
            
        }

        public virtual T Update(int id, TUpdate request)
        {
            TDb entity = dbSet.Find(id);

            mapper.Map(request, entity);

            if (entity != null)
            {
                BeforeUpdate(request, entity);
                dbSet.Update(entity);
                context.SaveChanges();
            }
            else
            {
                return null;
            }

            return mapper.Map<T>(entity);
        }

        public virtual void BeforeUpdate(TUpdate request, TDb entity)
        {

        }
    }
}
