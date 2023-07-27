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

        [HttpPost, Route("user")]
        public async Task<int> AddUserAsync(UserDto.Request.Add dto)
        {
            return await _userService.AddUserAsync(dto, dto.Password);
        }
    }
}
