using Common.Interfaces;
using Domain.Entities.Identity;
using Mapster;

namespace Services.IdentityServices.Interfaces
{
    public interface IUserService<TUser, TKey>
        where TKey : IEquatable<TKey>
    {
        Task<TKey> AddUserAsync<TDto>(TDto dto, string password, TypeAdapterConfig? cnf = null);

        Task UpdateUserAsync<TDto>(TDto dto, TypeAdapterConfig? cnf = null) where TDto : IIdHas<TKey>;

        Task DeleteUserAsync(TKey id);

        Task RemoveUserAsync(int id);
    }
}
