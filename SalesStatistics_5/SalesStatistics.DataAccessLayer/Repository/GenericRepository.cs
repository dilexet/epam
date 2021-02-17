using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SalesStatistics.DataAccessLayer.EntityFrameworkContext;
using Serilog;

namespace SalesStatistics.DataAccessLayer.Repository
{
    public class GenericRepository: IRepository
    {
        public SalesInformationContext Context { get; }

        public GenericRepository(SalesInformationContext context)
        {
            Context = context;
        }

        public IEnumerable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity: class
        {
            return predicate != null ? Context.Set<TEntity>().Where(predicate).AsEnumerable() : Context.Set<TEntity>().AsEnumerable();
        }

        public TEntity SingleOrDefault<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity: class
        {
            return Context.Set<TEntity>().Where(predicate).SingleOrDefault();
        }

        public TEntity Find<TEntity>(int? id) where TEntity: class
        {
            return Context.Set<TEntity>().Find(id);
        }
        
        public void Add<TEntity>(TEntity entity) where TEntity: class
        {
            if (entity == null)
            {
                Log.Error("Attempt to add invalid data");
                return;
            }
            Context.Set<TEntity>().Add(entity);
        }

        public void Remove<TEntity>(TEntity entity) where TEntity: class
        {
            if (entity == null)
            {
                Log.Error("Attempt to remove invalid data");
                return;
            }
            Context.Set<TEntity>().Remove(entity);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}