using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity> : IEntityRepository<TEntity>
    where TEntity : BaseEntity
    {
        protected DbContext context;
        public EfEntityRepositoryBase(DbContext context)
        {
            this.context = context;
        }
        public async Task<TEntity> Add(TEntity entity)
        {
            var addedEntity = await context.AddAsync(entity);
            return addedEntity.Entity;
        }

        public async Task Delete(TEntity entity)
        {
            await Task.Run(() =>
            {
                var deletedEntry = context.Entry(entity);
                deletedEntry.Entity.IsDeleted = true;
            });
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            return await context.Set<TEntity>().SingleOrDefaultAsync(filter);
        }

        public async Task<TEntity> GetById(long id)
        {
            return await Get(entity => entity.Id == id);
        }

        public async Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>>? filter = null)
        {
            return filter == null ? await context.Set<TEntity>().ToListAsync()
                : await context.Set<TEntity>().Where(filter).ToListAsync();
        }

        public async Task Update(TEntity entity)
        {
            await Task.Run(() =>
            {
                var updatedEntry = context.Entry(entity);
                updatedEntry.State = EntityState.Modified;
            });
        }
    }
}
