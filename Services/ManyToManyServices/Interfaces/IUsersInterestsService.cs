using Domain.Entities;
using Domain.Entities.Identity;

namespace Services.ManyToManyServices.Interfaces
{
    public interface IUsersInterestsService : IManyToManyService<User, Interest>
    {
        Task AttachInterestsToUserAsync(User user, HashSet<int> interestIds);
    }
}
