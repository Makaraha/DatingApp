using Common.Exceptions.ServerExceptions;
using Common.Interfaces;
using Domain.Entities.Identity;
using Domain.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Services.Services;

namespace Services.IdentityServices
{
    public class UserService : IUserService<int>
    {
        private UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<int> AddUserAsync<TDto>(TDto dto, string password, TypeAdapterConfig? cnf = null)
        {
            cnf = GetConfig(cnf);

            var user = dto.Adapt<User>(cnf);

            BeforeAdd(user);
            CheckIdentityResult(await _userManager.CreateAsync(user));
            CheckIdentityResult(await _userManager.AddPasswordAsync(user, password));

            return user.Id;
        }

        public async Task UpdateUserAsync<TDto>(TDto dto, TypeAdapterConfig? cnf = null)
            where TDto : IIdHas<int>
        {
            cnf = GetConfig(cnf);

            var user = await _userManager.FindByIdAsync(dto.Id.ToString());
            CheckEntity(user);

            dto.Adapt(user, cnf);
            BeforeUpdate(user);
            CheckIdentityResult(await _userManager.UpdateAsync(user));
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            CheckEntity(user);
            CheckIdentityResult(await _userManager.DeleteAsync(user));
        }

        public async Task RemoveUserAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            CheckEntity(user);
            user.IsDeleted = true;
            CheckIdentityResult(await _userManager.UpdateAsync(user));
        }

        protected TypeAdapterConfig GetConfig(TypeAdapterConfig? cnf)
        {
            return cnf ?? TypeAdapterConfig.GlobalSettings;
        }

        protected void CheckEntity(User? entity)
        {
            if (entity == null)
                throw new NotFoundException($"Entity {nameof(User)} not found.");
        }

        protected void BeforeAdd(User entity)
        {
            entity.CreatedDateUtc = DateTime.UtcNow;
        }

        protected void BeforeUpdate(User entity)
        {
            entity.LastModifiedDateUtc = DateTime.UtcNow;
        }

        protected void CheckIdentityResult(IdentityResult result)
        {
            if (!result.Succeeded)
                throw new Exception(String.Join('\n', result.Errors));
        }
    }
}
