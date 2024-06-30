#pragma warning disable CS0108

using MediClubApp.Models;
using MediClubApp.Repositories.Base.CRUD;

namespace MediClubApp.Repositories.Base;

public interface IUserRepository :  ICreateAsync<User>, IGetAsync<User>, IGetAllAsync<User>, IUpdateAsync<User>, IDeleteAsync<User>
{
    Task<User?> GetAsync(Guid id);
    Task<IEnumerable<User>> GetAllAsync();
    Task CreateAsync(User newUser);
    Task UpdateAsync(Guid id, User newUser);
    Task DeleteByIdAsync(Guid id);
}