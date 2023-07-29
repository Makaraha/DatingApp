using Domain.Entities.Identity;
using DTOs;
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
        /// Create user
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>User id</returns>
        [HttpPost, Route("user"), AllowAnonymous]
        public async Task<int> AddUserAsync(UserDto.Request.Add dto)
        {
            return await _userService.AddUserAsync(dto, dto.Password);
        }

        [HttpPut, Route("user"), Authorize(Roles = "admin")]
        public async Task UpdateUserAsync(UserDto.Request.Update dto)
        {
            await _userService.UpdateUserAsync(dto);
        }

        [HttpPatch, Route("user/remove/{id}"), Authorize(Roles = "admin")]
        public async Task RemoveUserAsync(int id)
        {
            await _userService.RemoveUserAsync(id);
        }
    }
}
