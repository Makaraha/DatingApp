using Common.Interfaces;
using Mapster;

namespace Services.IdentityServices.Interfaces
{
    public interface IUserService<TUser, TKey>
        where TKey : IEquatable<TKey>
    {
        Task<List<TDto>> GetUsersAsync<TDto>(int? genderId, int? searchingGenderId, IEnumerable<int>? interests,
            int? ageLessThan, int? ageMoreThan, TypeAdapterConfig? cnf = null);

        Task<TKey> AddUserAsync<TDto>(TDto dto, string password, TypeAdapterConfig? cnf = null);

        Task UpdateUserAsync<TDto>(TDto dto, TypeAdapterConfig? cnf = null) where TDto : IIdHas<TKey>;

        Task DeleteUserAsync(TKey id);

        Task RemoveUserAsync(int id);

        Task<TDto> GetByIdAsync<TDto>(int id, TypeAdapterConfig? cnf = null);
    }
}
