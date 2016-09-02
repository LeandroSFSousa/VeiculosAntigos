using System;
using System.Collections.Generic;
using VeiculosAntigos.Repository.Base;

namespace VeiculosAntigos.Service.Base
{
    public abstract class BaseService<TEntity> where TEntity : class
    {
        protected readonly IGenericRepository<TEntity> _rep;

        public BaseService(IGenericRepository<TEntity> repository)
        {
            _rep = repository;
        }

        public virtual IEnumerable<TEntity> Get()
        {
            return _rep.Get();
        }

        public virtual TEntity Get(object id)
        {
            return _rep.Get(id);
        }

        public virtual bool Insert(TEntity entity)
        {
            _rep.Insert(entity);
            return _rep.Save() > 0;
        }

        public virtual bool Update(object id, TEntity entity)
        {
            _rep.Update(id, entity);
            return _rep.Save() > 0;
        }

        public virtual bool Delete(object id)
        {
            _rep.Delete(id);
            return _rep.Save() > 0;
        }

        protected bool Exists(object id)
        {
            return _rep.Get(id) != null;
        }
    }
}
