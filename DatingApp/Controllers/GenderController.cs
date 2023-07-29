using Domain.Entities;
using DTOs;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.IService;

namespace DatingApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class GenderController : BaseController
    {
        private readonly ITranslationHasService<Gender> _genderService;

        public GenderController(ITranslationHasService<Gender> genderService)
        {
            _genderService = genderService;
        }

        [AllowAnonymous]
        [HttpGet, Route("gender/{id}")]
        public async Task<GenderDto.Response.ById> GetByIdAsync(int id, string culture = "ru-RU")
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<Gender, GenderDto.Response.ById>()
                .Map(dest => dest.Name, src => src.GetLocalizedName());

            return await _genderService.GetByIdAsync<GenderDto.Response.ById>(id, config);
        }

        [AllowAnonymous]
        [HttpGet, Route("genders")]
        public async Task<List<GenderDto.Response.List>> GetListAsync(string culture = "ru-RU")
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<Gender, GenderDto.Response.List>()
                .Map(dest => dest.Name, src => src.GetLocalizedName());

            return await _genderService.GetListAsync<GenderDto.Response.List>(config);
        }

        [AllowAnonymous]
        [HttpGet, Route("gender/translations/{genderId}")]
        public async Task<List<TranslationDto>> GetTranslationsAsync(int genderId)
        {
            return await _genderService.GetTranslations<TranslationDto>(genderId);
        }

        [HttpPost, Route("gender/translation")]
        public async Task AddTranslationAsync(GenderDto.Request.AddTranslation dto)
        {
            await _genderService.AddOrEditTranslation(dto, dto.GenderId);
        }
    }
}
