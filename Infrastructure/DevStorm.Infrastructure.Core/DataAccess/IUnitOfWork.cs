using DevStorm.Infrastructure.Core.Domain;

namespace DevStorm.Infrastructure.Core.DataAccess
{
    public interface IUnitOfWork
    {
        void Commit();
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity, new();
    }
}