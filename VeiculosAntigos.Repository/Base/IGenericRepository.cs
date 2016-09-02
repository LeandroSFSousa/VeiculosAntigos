using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace VeiculosAntigos.Repository.Base
{
    public interface IGenericRepository<TEntity> where TEntity: class
    {

        IEnumerable<TEntity> Get();
        TEntity Get(object id);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
        void Insert(TEntity entity);
        void Update(object id, TEntity entity);
        void Delete(TEntity entity);
        void Delete(object id);
        int Save();
    }
}
