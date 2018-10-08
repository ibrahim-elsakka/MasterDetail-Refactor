using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace MasterDetailExample.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext DbContext;
        
        public Repository(DbContext context)
        {
            DbContext = context;
        }
        
        public virtual IQueryable<TEntity> GetAll()
        {
            return DbContext.Set<TEntity>();
        }
        
        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbContext.Set<TEntity>().AsNoTracking().Where(predicate);
        }
        
        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return DbContext.Set<TEntity>().Count(predicate);
        }
        
        public int Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            DbContext.Set<TEntity>().Add(entity);
            return DbContext.SaveChanges();
        }
        
        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }
        
        public int Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity Issue. It''s null.");
            DbContext.Entry(entity).State = EntityState.Deleted;
            return DbContext.SaveChanges();
        }
        
        public TEntity First(Expression<Func<TEntity, bool>> predicate)
        {
            return DbContext.Set<TEntity>().FirstOrDefault(predicate);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (DbContext == null) return;
            DbContext.Dispose();
            DbContext = null;
        }
    }
}