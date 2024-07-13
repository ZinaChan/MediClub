using MediClubApp.Models;
namespace MediClubApp.Services.Base;

public interface IUserService
{
    Task<User?> GetUserAsync(Guid id);
    Task<User?> GetUserAsync(string email);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task CreateUserAsync(User newUser, IFormFile image);
    Task UpdateUserAsync(Guid id, User newUser);
    Task DeleteUserByIdAsync(Guid id);
    Task<string> ChangeAvatar(Guid id, IFormFile formFile);
}

