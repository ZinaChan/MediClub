#pragma warning disable CS0108

using MediClubApp.Models;
using MediClubApp.Repositories.Base.CRUD;

namespace MediClubApp.Repositories.Base;

public interface IUserRepository :  IGetAsync<User>, IGetAllAsync<User>, IUpdateAsync<User>, IDeleteAsync<User>
{
    Task<User?> GetAsync(Guid id);
    Task<User?> GetAsync(string email);
    Task<IEnumerable<User>> GetAllAsync();
    Task CreateAsync(User newUser, IFormFile image);
    Task UpdateAsync(Guid id, User newUser);
    Task DeleteByIdAsync(Guid id);
}