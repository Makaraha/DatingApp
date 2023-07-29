using System.Security.Cryptography;
using Domain.Entities.Translations.Base;
using Domain.Entities.Translations.ITranslation;
using Domain.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Repository.IRepository;
using Services.IService;

namespace Services.Services
{
    public class TranslationHasService<TEntity, TTranslation> : BaseService<TEntity, int>, ITranslationHasService<TEntity>
        where TTranslation : class, ITranslation
        where TEntity : TranslationHasEntity<TTranslation>
    {
        private ITranslationHasRepository<TEntity, TTranslation> _translationHasRepository;

        public TranslationHasService(ITranslationHasRepository<TEntity, TTranslation> repository) : base(repository)
        {
            _translationHasRepository = repository;    
        }

        public async Task AddOrEditTranslation<TDto>(TDto dto, int entityId, TypeAdapterConfig? cnf = null)
        {
            cnf = GetConfig(cnf);

            var translation = dto.Adapt<TTranslation>();
            var query = Repository.GetAllAsQuery().Include(x => x.Translations);
            var entity = await Repository.GetByIdAsync(entityId, query);
            CheckEntity(entity);

            var oldTranslation = entity.Translations.FirstOrDefault(x => x.CultureName == translation.CultureName);
            if(oldTranslation != null)
                oldTranslation.LocalizedName = translation.LocalizedName;
            else
                _translationHasRepository.AddTranslation(translation, entity);

            await Repository.SaveChangesAsync();
        }
    }
}
