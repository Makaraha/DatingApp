using Domain.EF.Context;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repository.IRepository;

namespace Repository.Repositories
{
    public class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TKey : IEquatable<TKey>
        where TEntity : class, IEntity<TKey>
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _entities;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAllAsQuery()
        {
            return _entities.AsQueryable();
        }

        public async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _entities.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task AddAsync(TEntity entity)
        {
            await _entities.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _entities.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public void Remove(TEntity entity)
        {
            entity.IsDeleted = true;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
