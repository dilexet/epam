using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Serilog;

namespace SalesStatistics.DataAccessLayer.Repository
{
    public class GenericRepository<TEntity>: IRepository<TEntity> where TEntity : class
    {
        private DbContext Context { get; }
        private DbSet<TEntity> EntitySet { get; }
        
        public GenericRepository(DbContext context)
        {
            Context = context;
            EntitySet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return predicate != null ? EntitySet.Where(predicate).AsEnumerable() : EntitySet.AsEnumerable();
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return EntitySet.Where(predicate).SingleOrDefault();
        }

        public void Add(TEntity entity)
        {
            if (entity == null)
            {
                Log.Error("Attempt to add invalid data");
                return;
            }
            EntitySet.Add(entity);
        }

        public void Remove(TEntity entity)
        {
            if (entity == null)
            {
                Log.Error("Attempt to remove invalid data");
                return;
            }
            EntitySet.Remove(entity);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}