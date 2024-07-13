using MediClubApp.Models;
using MediClubApp.Repositories.Base;
using MediClubApp.Services.Base;

namespace MediClubApp.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        this._userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }
    public async Task CreateUserAsync(User newUser, IFormFile image)
    {
        if (newUser is null && this.GetUserAsync(newUser!.Email) is not null)
        {
            throw new ArgumentNullException(nameof(newUser));
        }

        await _userRepository.CreateAsync(newUser: newUser, image: image);
    }
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }
    public async Task<User?> GetUserAsync(Guid id)
    {
        return await this._userRepository.GetAsync(id: id);
    }
    public async Task<User?> GetUserAsync(string email)
    {
        return await this._userRepository.GetAsync(email: email);
    }
    public async Task UpdateUserAsync(Guid id, User newUser)
    {

        if (newUser is null || await this._userRepository.GetAsync(id) is null)
        {
            throw new ArgumentNullException(nameof(newUser));
        }

        await _userRepository.UpdateAsync(id: id, newUser: newUser);
    }
    public async Task DeleteUserByIdAsync(Guid id)
    {
        if (await this._userRepository.GetAsync(id) is null)
        {
            throw new ArgumentNullException(nameof(User));
        }

        await _userRepository.DeleteByIdAsync(id: id);
    }

    public async Task<string> ChangeAvatar(Guid id, IFormFile formFile)
    {
        if (await this._userRepository.GetAsync(id) is null)
        {
            throw new ArgumentNullException(nameof(User));
        }

        return await _userRepository.ChangeAvatar(id: id, formFile: formFile);
    }
}

