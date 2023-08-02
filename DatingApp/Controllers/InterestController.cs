using Domain.Entities;
using DTOs;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.IService;

namespace DatingApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class InterestController : BaseController
    {
        private readonly ITranslationHasService<Interest> _interestService;

        public InterestController(ITranslationHasService<Interest> interestService)
        {
            _interestService = interestService;
        }

        [AllowAnonymous]
        [HttpGet, Route("interest/{id}")]
        public async Task<InterestDto.Response.ById> GetByIdAsync(int id, string culture = "ru-RU")
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<Interest, InterestDto.Response.ById>()
                .Map(dest => dest.Name, src => src.Translations.GetLocalizedName() ?? src.Name);

            return await _interestService.GetByIdAsync<InterestDto.Response.ById>(id, config);
        }

        [AllowAnonymous]
        [HttpGet, Route("interests")]
        public async Task<List<InterestDto.Response.List>> GetListAsync(string culture = "ru-RU")
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<Interest, InterestDto.Response.List>()
                .Map(dest => dest.Name, src => src.Translations.GetLocalizedName() ?? src.Name);

            return await _interestService.GetListAsync<InterestDto.Response.List>(config);
        }

        [AllowAnonymous]
        [HttpGet, Route("interest/translations/{interestId}")]
        public async Task<List<TranslationDto>> GetTranslationsAsync(int interestId)
        {
            return await _interestService.GetTranslations<TranslationDto>(interestId);
        }

        [HttpPost, Route("interest/translation")]
        public async Task AddTranslationAsync(GenderDto.Request.AddTranslation dto)
        {
            await _interestService.AddOrEditTranslation(dto, dto.GenderId);
        }
    }
}
