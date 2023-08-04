using Domain.Entities;
using Domain.Entities.Identity;

namespace Services.ManyToManyServices.Interfaces
{
    public interface IUsersInterestsService : IManyToManyService<User, Interest>
    {
        Task AddInterestsToUserAsync(User user, HashSet<int> interestIds);

        Task UpdateInterestsForUserAsync(User user, HashSet<int> interestIds);
    }
}
