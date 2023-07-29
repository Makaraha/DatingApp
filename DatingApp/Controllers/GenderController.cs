using Domain.Entities;
using DTOs;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Services.IService;

namespace DatingApp.Controllers
{
    public class GenderController : BaseController
    {
        private readonly ITranslationHasService<Gender> _genderService;

        public GenderController(ITranslationHasService<Gender> genderService)
        {
            _genderService = genderService;
        }

        [HttpGet, Route("gender/{id}")]
        public async Task<GenderDto.Response.ById> GetByIdAsync(int id)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<Gender, GenderDto.Response.ById>()
                .Map(dest => dest.Name, src => src.GetLocalizedName());

            return await _genderService.GetByIdAsync<GenderDto.Response.ById>(id, config);
        }

        [HttpGet, Route("genders")]
        public async Task<List<GenderDto.Response.List>> GetListAsync()
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<Gender, GenderDto.Response.List>()
                .Map(dest => dest.Name, src => src.GetLocalizedName());

            return await _genderService.GetListAsync<GenderDto.Response.List>();
        }
    }
}
