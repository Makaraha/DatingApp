﻿using Common.Exceptions.ServerExceptions;
using Common.Interfaces;
using Domain.EF.Context;
using Domain.Entities.Identity;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository.IRepository;
using Services.IdentityServices.Interfaces;
using Services.ManyToManyServices.Interfaces;

namespace Services.IdentityServices
{
    public class UserService : IUserService<User, int>
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepository<User, int> _userRepository;

        private readonly IUsersInterestsService _usersInterestsService;

        public UserService(UserManager<User> userManager, IRepository<User, int> userRepository, IUsersInterestsService usersInterestsService)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _usersInterestsService = usersInterestsService;
        }

        public async Task<List<TDto>> GetUsersAsync<TDto>(int? genderId, int? searchingGenderId, IEnumerable<int>? interests, int? ageLessThan,
            int? ageMoreThan, TypeAdapterConfig? cnf = null)
        {
            cnf = GetConfig(cnf);
            var now = DateTime.UtcNow;

            var query = _userRepository.GetAllAsQuery()
                .Include(x => x.Gender)
                .Include(x => x.SearchingGender)
                .Include(x => x.Interests)
                .AsQueryable();

            if (genderId != null)
                query = query.Where(x => x.GenderId == genderId.Value);

            if (searchingGenderId != null)
                query = query.Where(x => x.SearchingGenderId == searchingGenderId.Value);

            if (interests != null && interests.Any())
                query = query.Where(x => x.Interests.Any(x => interests.Contains(x.Id)));

            if (ageLessThan != null)
            {
                var birthDateMoreThan = now.AddYears(-ageLessThan.Value);
                query = query.Where(x => x.DateOfBirth > birthDateMoreThan);
            }

            if (ageMoreThan != null)
            {
                var birthDateLessThan = now.AddYears(-ageMoreThan.Value);
                query = query.Where(x => x.DateOfBirth < birthDateLessThan);
            }

            return await query.ProjectToType<TDto>(cnf).ToListAsync();
        }

        public async Task<int> AddUserAsync<TDto>(TDto dto, string password, TypeAdapterConfig? cnf = null)
        {
            cnf = GetConfig(cnf);

            var user = dto.Adapt<User>(cnf);

            await BeforeAddAsync(user);

            CheckIdentityResult(await _userManager.CreateAsync(user));
            await SetPasswordOrDeleteAsync(user, password);

            return user.Id;
        }

        public async Task UpdateUserAsync<TDto>(TDto dto, TypeAdapterConfig? cnf = null)
            where TDto : IIdHas<int>
        {
            cnf = GetConfig(cnf);

            var query = _userRepository.GetAllAsQuery()
                .Include(x => x.Interests);
            var user = await _userRepository.GetByIdAsync(dto.Id, query);
            CheckEntity(user);

            dto.Adapt(user, cnf);
            await BeforeUpdateAsync(user);

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

        public async Task<TDto> GetByIdAsync<TDto>(int id, TypeAdapterConfig? cnf = null)
        {
            cnf = GetConfig(cnf);

            var query = _userRepository.GetAllAsQuery()
                .Include(x => x.Gender)
                .Include(x => x.SearchingGender)
                .Include(x => x.Interests);

            var user = await _userRepository.GetByIdAsync(id, query);
            CheckEntity(user);

            return user.Adapt<TDto>();
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

        protected async Task BeforeAddAsync(User entity)
        {
            await _usersInterestsService.AddInterestsToUserAsync(entity, entity.Interests.Select(x => x.Id).ToHashSet());
            entity.CreatedDateUtc = DateTime.UtcNow;
        }

        protected async Task BeforeUpdateAsync(User entity)
        {
            await _usersInterestsService.UpdateInterestsForUserAsync(entity,
                entity.Interests.Select(x => x.Id).ToHashSet());
            entity.LastModifiedDateUtc = DateTime.UtcNow;
        }

        protected void CheckIdentityResult(IdentityResult result)
        {
            if (!result.Succeeded)
                throw new BadRequestException(String.Join('\n', result.Errors.Select(x => x.Description)));
        }

        protected async Task SetPasswordOrDeleteAsync(User user, string password)
        {
            try
            {
                CheckIdentityResult(await _userManager.AddPasswordAsync(user, password));
            }
            catch
            {
                await _userManager.DeleteAsync(user);
                throw;
            }
        }
    }
}
