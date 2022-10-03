using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using Core.Entities;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;

namespace DataAccess.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        public ShoppingSystemContext Context { get; }

        public UnitOfWork(ShoppingSystemContext context)
        {
            Context = context;
        }

        public IEntityRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            return new EfEntityRepositoryBase<TEntity>(Context);
        }

        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {

        }
    }
}
