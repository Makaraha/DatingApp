using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Identity;
using Repository.IRepository;

namespace Services.Services
{
    public class UserService : BaseService<User, int>
    {
        public UserService(IRepository<User, int> repository) : base(repository)
        {
        }
    }
}
