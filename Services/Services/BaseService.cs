using System.Security.Cryptography;
using Common.Exceptions.ServerExceptions;
using Common.Interfaces;
using Domain.Interfaces;
using Mapster;
using Repository.IRepository;
using Services.IService;

namespace Services.Services
{
    public class BaseService<TEntity, TKey> : IService<TEntity, TKey>
        where TKey : IEquatable<TKey>
        where TEntity : class, IEntity<TKey>
    {
        protected readonly IRepository<TEntity, TKey> Repository;

        public BaseService(IRepository<TEntity, TKey> repository)
        {
            Repository = repository;
        }

        public async Task<TDto?> GetByIdAsync<TDto>(TKey id, TypeAdapterConfig? cnf = null)
        {
            cnf = GetConfig(cnf);

            var entity = await Repository.GetByIdAsync(id);
            CheckEntity(entity);

            return entity.Adapt<TDto>(cnf);
        }

        public async Task<TKey> AddAsync<TDto>(TDto dto, TypeAdapterConfig? cnf = null)
        {
            cnf = GetConfig(cnf);

            var entity = dto.Adapt<TEntity>(cnf);

            BeforeAdd(entity);
            await Repository.AddAsync(entity);
            await Repository.SaveChangesAsync();

            return entity.Id;
        }

        public async Task Update<TDto>(TDto dto, TypeAdapterConfig? cnf = null)
            where TDto : IIdHas<TKey>
        {
            cnf = GetConfig(cnf);

            var entity = await Repository.GetByIdAsync(dto.Id);
            CheckEntity(entity);

            BeforeUpdate(entity);
            dto.Adapt(entity, cnf);
            Repository.Update(entity);

            await Repository.SaveChangesAsync();
        }

        public async Task Delete(TKey id)
        {
            var entity = await Repository.GetByIdAsync(id);
            CheckEntity(entity);

            Repository.Delete(entity);
            await Repository.SaveChangesAsync();
        }

        public async Task Remove(TKey id)
        {
            var entity = await Repository.GetByIdAsync(id);
            CheckEntity(entity);

            Repository.Remove(entity);
            await Repository.SaveChangesAsync();
        }

        protected TypeAdapterConfig GetConfig(TypeAdapterConfig? cnf)
        {
            return cnf ?? TypeAdapterConfig.GlobalSettings;
        }

        protected void CheckEntity(TEntity? entity)
        {
            if (entity == null)
                throw new NotFoundException($"Entity {typeof(TEntity)} not found.");
        }

        protected void BeforeAdd(TEntity entity)
        {
            entity.CreatedDateUtc = DateTime.UtcNow;
        }

        protected void BeforeUpdate(TEntity entity)
        {
            entity.LastModifiedDateUtc = DateTime.UtcNow;
        }
    }
}
