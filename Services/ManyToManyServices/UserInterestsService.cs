using Common.Exceptions.ServerExceptions;
using Domain.Entities;
using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Repository.IRepository;
using Services.ManyToManyServices.Interfaces;

namespace Services.ManyToManyServices
{
    public class UserInterestsService : IUsersInterestsService
    {
        private IRepository<Interest, int> _interestRepository;

        public UserInterestsService(IRepository<Interest, int> interestRepository)
        {
            _interestRepository = interestRepository;
        }

        public async Task AttachInterestsToUserAsync(User user, HashSet<int> interestIds)
        {
            var interests = await _interestRepository.GetAllAsQuery().Where(x => interestIds.Contains(x.Id)).ToListAsync();

            if (interests.Count() != interestIds.Count())
                throw new NotFoundException("Wrong interest id");

            user.Interests = interests;
        }
    }
}
