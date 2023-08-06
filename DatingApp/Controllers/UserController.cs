using Domain.Entities;
using Domain.Entities.Identity;
using DTOs;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.IdentityServices.Interfaces;

namespace DatingApp.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUserService<User, int> _userService;

        public UserController(IUserService<User, int> userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get users
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet, Route("users"), AllowAnonymous]
        public async Task<List<UserDto.Response.List>> GetUsersAsync([FromQuery]IEnumerable<int>? interests, int? genderId, int? searchingGenderId, int? ageMoreThan, int? ageLessThan, string? culture = "ru-RU")
        {
            var cnf = new TypeAdapterConfig();
            cnf.NewConfig<Gender, UserDto.GenderDto>()
                .Map(dest => dest.Name, src => src.Translations.GetLocalizedName() ?? src.Name);
            cnf.NewConfig<Interest, UserDto.InterestDto>()
                .Map(dest => dest.Name, src => src.Translations.GetLocalizedName() ?? src.Name);

            return await _userService.GetUsersAsync<UserDto.Response.List>(genderId, searchingGenderId, interests,
                ageLessThan, ageMoreThan);
        }

        /// <summary>
        /// Get user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("user"), AllowAnonymous]
        public async Task<UserDto.Response.ById> GetUserAsync(int id, string? culture = "ru-RU")
        {
            var cnf = new TypeAdapterConfig();
            cnf.NewConfig<Gender, UserDto.GenderDto>()
                .Map(dest => dest.Name, src => src.Translations.GetLocalizedName() ?? src.Name);
            cnf.NewConfig<Interest, UserDto.InterestDto>()
                .Map(dest => dest.Name, src => src.Translations.GetLocalizedName() ?? src.Name);

            return await _userService.GetByIdAsync<UserDto.Response.ById>(id, cnf);
        }

        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>User id</returns>
        [HttpPost, Route("user"), AllowAnonymous]
        public async Task<int> AddUserAsync(UserDto.Request.Add dto)
        {
            return await _userService.AddUserAsync(dto, dto.Password);
        }

        /// <summary>
        /// Edit user
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut, Route("user"), Authorize(Roles = "admin")]
        public async Task UpdateUserAsync(UserDto.Request.Update dto)
        {
            await _userService.UpdateUserAsync(dto);
        }

        /// <summary>
        /// User soft delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch, Route("user/remove/{id}"), Authorize(Roles = "admin")]
        public async Task RemoveUserAsync(int id)
        {
            await _userService.RemoveUserAsync(id);
        }
    }
}
