using Domain.EF.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repository.IRepository;

namespace Repository.Repositories
{
    public class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TKey : IEquatable<TKey>
        where TEntity : class, IEntity<TKey>
    {
        protected readonly ApplicationDbContext Context;
        protected readonly DbSet<TEntity> Entities;

        public BaseRepository(ApplicationDbContext context)
        {
            Context = context;
            Entities = context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAllAsQuery()
        {
            return Entities.AsQueryable();
        }

        public async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await GetByIdAsync(id, Entities);
        }

        public async Task<TEntity?> GetByIdAsync(TKey id, IQueryable<TEntity> query)
        {
            return await query.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task AddAsync(TEntity entity)
        {
            await Entities.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            Entities.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            Entities.Remove(entity);
        }

        public void Remove(TEntity entity)
        {
            entity.IsDeleted = true;
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Entities.RemoveRange(entities);
        }

        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
