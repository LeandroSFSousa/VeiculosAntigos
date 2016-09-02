using System.Collections.Generic;

namespace VeiculosAntigos.Service.Base
{
    public interface IBaseService<TEntity> where TEntity: class
    {
        IEnumerable<TEntity> Get();
        TEntity Get(object id);
        bool Insert(TEntity entity);
        bool Update(object id, TEntity entity);
        bool Delete(object id);
    }
}
