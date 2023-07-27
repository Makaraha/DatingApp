using Domain.Entities.Identity;
using Services.IService;

namespace DatingApp.Controllers
{
    public class UserController : BaseController
    {
        private IService<User, int> _userService;

        public UserController(IService<User, int> userService)
        {
            _userService = userService;
        }

        public async Task<>
    }
}
