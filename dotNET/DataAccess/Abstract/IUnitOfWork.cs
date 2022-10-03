using Core.DataAccess;
using Core.Entities;
using DataAccess.Concrete.Contexts;

namespace DataAccess.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        public ShoppingSystemContext Context { get; }
        public IEntityRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
        public Task SaveAsync();
    }
}