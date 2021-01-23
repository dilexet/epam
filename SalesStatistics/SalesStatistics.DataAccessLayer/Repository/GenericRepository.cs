using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

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
        
        public IQueryable<TEntity> Get()
        {
            return EntitySet;
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return EntitySet.Where(predicate);
        }

        public void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            EntitySet.Add(entity);
        }

        public void Remove(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            EntitySet.Remove(entity);
        }

        public void Attach(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            EntitySet.Attach(entity);
        }

        public void Reload(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            Context.Entry(entity).Reload();
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        
        private bool _disposed;

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}