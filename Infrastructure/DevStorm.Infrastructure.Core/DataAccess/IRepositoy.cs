using System;
using System.Linq;
using System.Linq.Expressions;
using DevStorm.Infrastructure.Core.Domain;

namespace DevStorm.Infrastructure.Core.DataAccess
{
    public interface IRepository<TEntity> where TEntity : class ,IEntity
    {
        void Add(TEntity entity);
        void Modify(TEntity entity);
        void Delete(TEntity entity);
        void DeleteById(object id);
        TEntity FindById(object id);
        TEntity Find(Expression<Func<TEntity, bool>> expression);
        IQueryable<TEntity> Query();
    }
}
