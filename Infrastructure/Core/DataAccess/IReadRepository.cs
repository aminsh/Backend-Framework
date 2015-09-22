using System;
using System.Linq;
using System.Linq.Expressions;

namespace Core.DataAccess
{
    public interface IReadRepository<TEntity> 
    {
        TEntity GetById(object id);
        TEntity Get(Expression<Func<TEntity, bool>> expression);
        IQueryable<TEntity> Get();
    }
}
