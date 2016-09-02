using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using VeiculosAntigos.Data.EF.Context;

namespace VeiculosAntigos.Repository.Base
{
    public abstract class GenericRepositoryEF<TEntity> where TEntity : class
    {
        internal VeiculosAntigosEFModel context;
        internal DbSet<TEntity> dbSet;

        public GenericRepositoryEF(VeiculosAntigosEFModel context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual IEnumerable<TEntity> Get()
        {
            return dbSet;
        }

        public virtual TEntity Get(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(object id, TEntity entityToUpdate)
        {
            var entityOriginal = dbSet.Find(id);
            var types = new Type[3] { typeof(string), typeof(int), typeof(Guid) };

            foreach (PropertyInfo propertyInfo in entityOriginal.GetType().GetProperties())
            {
                if (types.Contains(propertyInfo.GetType()))
                {
                    if (propertyInfo.GetValue(entityToUpdate, null) == null)
                        propertyInfo.SetValue(entityToUpdate, propertyInfo.GetValue(entityOriginal, null), null);
                }
            }

            context.Entry(entityOriginal).CurrentValues.SetValues(entityToUpdate);
        }

        public virtual int Save()
        {
            return context.SaveChanges();
        }
    }
}
