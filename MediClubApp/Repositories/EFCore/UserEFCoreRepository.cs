#pragma warning disable CS1998

using MediClubApp.Models;
using MediClubApp.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using MediClubApp.Repositories.EFCore.Dbcontext;

namespace MediClubApp.Repositories.EFCore;

public class UserEFCoreRepository : IUserRepository
{
    private readonly MyClinicDbContext _clinicDbContext;

    public UserEFCoreRepository(MyClinicDbContext dbContext)
    {
        this._clinicDbContext = dbContext;
    }

    public async Task CreateAsync(User newUser, IFormFile image)
    {
        newUser.Id = Guid.NewGuid();
        newUser.Role = newUser.Role == default ? UserRole.Patient.ToString() : newUser.Role;
        var extension = new FileInfo(image.FileName).Extension[1..];
        newUser.AvatarUrl = $"Assets/UsersImg/{newUser.Id}.{extension}";

        var res = await this._clinicDbContext.Users.AddAsync(entity: newUser);
        await this._clinicDbContext.SaveChangesAsync();


        if (this.GetAsync(newUser.Email) is not null)
        {
            var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var uploadsFolder = Path.Combine(wwwrootPath, "Assets", "UsersImg");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var filePath = Path.Combine(uploadsFolder, $"{newUser.Id}.{extension}");

            using var newFileStream = System.IO.File.Create(filePath);
            await image.CopyToAsync(newFileStream);
        }
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var oldUser = await this._clinicDbContext.Users.FirstOrDefaultAsync(d => d.Id == id);

        if (oldUser is null) return;

        this._clinicDbContext.Users.Remove(entity: oldUser);
        await this._clinicDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var users = await this._clinicDbContext.Users.ToListAsync();
        return users;
    }

    public Task<User?> GetAsync(Guid id)
    {
        return this._clinicDbContext.Users.FirstOrDefaultAsync(d => d.Id == id);
    }
    public Task<User?> GetAsync(string email)
    {
        return this._clinicDbContext.Users.FirstOrDefaultAsync(d => d.Email == email);
    }

    public async Task UpdateAsync(Guid id, User newUser)
    {
        var oldUser = await this._clinicDbContext.Users.FirstOrDefaultAsync(d => d.Id == id);
        if (oldUser is null) return;

        oldUser.Password = newUser.Password;
        oldUser.Email = newUser.Email;
        oldUser.FirstName = newUser.FirstName;
        oldUser.LastName = newUser.LastName;
        oldUser.Address = newUser.Address;
        oldUser.DateOfBirth = newUser.DateOfBirth;
        oldUser.Gender = newUser.Gender;
        oldUser.PhoneNumber = newUser.PhoneNumber;
        oldUser.AvatarUrl = newUser.AvatarUrl;

        this._clinicDbContext.Update(oldUser);
        await this._clinicDbContext.SaveChangesAsync();
    }
}

