using Domain.Entities.Identity;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Services.IdentityServices;
using Services.IService;

namespace DatingApp.Controllers
{
    public class UserController : BaseController
    {
        private IUserService<int> _userService;

        public UserController(IUserService<int> userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>User id</returns>
        [HttpPost, Route("user")]
        public async Task<int> AddUserAsync(UserDto.Request.Add dto)
        {
            return await _userService.AddUserAsync(dto, dto.Password);
        }

        [HttpPut, Route("user")]
        public async Task UpdateUserAsync(UserDto.Request.Update dto)
        {
            await _userService.UpdateUserAsync(dto);
        }

        [HttpPatch, Route("user/remove/{id}")]
        public async Task RemoveUserAsync(int id)
        {
            await _userService.RemoveUserAsync(id);
        }

        [HttpDelete, Route("user/delete/{id}")]
        public async Task DeleteUserAsync(int id)
        {
            await _userService.DeleteUserAsync(id);
        }
    }
}
