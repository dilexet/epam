using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SalesStatistics.DataAccessLayer
{
    public interface IRepository<TEntity> where TEntity: class
    {
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null);
        
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        
        void Add(TEntity entity);
        
        void Remove(TEntity entity);
        
        void Save();
    }
}