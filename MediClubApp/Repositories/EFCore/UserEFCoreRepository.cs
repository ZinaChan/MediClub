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

    public async Task<string> ChangeAvatar(Guid id, IFormFile avatarFile)
    {
        var user = await _clinicDbContext.Users.FindAsync(id);
        if (user == null)
        {
            throw new ArgumentException($"User with ID '{id}' not found.");
        }

        // Remove old avatar file if it exists
        var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        var uploadsFolder = Path.Combine(wwwrootPath, "Assets", "UsersImg");

        if (!string.IsNullOrEmpty(user.AvatarUrl))
        {
            var oldAvatarPath = Path.Combine(uploadsFolder, Path.GetFileName(user.AvatarUrl));
            if (File.Exists(oldAvatarPath))
            {
                File.Delete(oldAvatarPath);
            }
        }

        // Save new avatar file
        var extension = Path.GetExtension(avatarFile.FileName);
        var newFileName = $"{id}{extension}";

        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var filePath = Path.Combine(uploadsFolder, newFileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await avatarFile.CopyToAsync(stream);
        }

        // Update user's AvatarUrl in database
        user.AvatarUrl = $"Assets/UsersImg/{newFileName}";
        _clinicDbContext.Users.Update(user);
        await _clinicDbContext.SaveChangesAsync();

        return user.AvatarUrl;
    }


    public async Task CreateAsync(User newUser, IFormFile image)
    {
        newUser.Id = Guid.NewGuid();
        newUser.Role = newUser.Role == default ? UserRole.Patient.ToString() : newUser.Role;
        if(image != null)
        {
            newUser.AvatarUrl = $"Assets/UsersImg/{newUser.Id}.{Path.GetExtension(image.FileName)}";
        }
        

        var res = await this._clinicDbContext.Users.AddAsync(entity: newUser);
        await this._clinicDbContext.SaveChangesAsync();


        if (this.GetAsync(newUser.Email) is not null && image != null)
        {
            var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var uploadsFolder = Path.Combine(wwwrootPath, "Assets", "UsersImg");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var filePath = Path.Combine(uploadsFolder, $"{newUser.Id}.{Path.GetExtension(image.FileName)}");

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
        if (oldUser == null)
            return;

        oldUser.FirstName = newUser.FirstName ?? oldUser.FirstName;
        oldUser.LastName = newUser.LastName ?? oldUser.LastName;
        oldUser.Address = newUser.Address ?? oldUser.Address;
        oldUser.PhoneNumber = newUser.PhoneNumber ?? oldUser.PhoneNumber;
        oldUser.Gender = newUser.Gender ?? oldUser.Gender;

        if (oldUser.Role != newUser.Role &&  newUser.Role != null)
        {
            switch (newUser.Role)
            {
                case "Patient":
                    var newPatient = new Patient
                    {
                        Id = oldUser.Id,
                        AvatarUrl = oldUser.AvatarUrl,
                        FirstName = oldUser.FirstName,
                        LastName = oldUser.LastName,
                        Address = oldUser.Address,
                        PhoneNumber = oldUser.PhoneNumber,
                        Gender = oldUser.Gender,
                        DateOfBirth = oldUser.DateOfBirth,
                        Email = oldUser.Email,
                        Password = oldUser.Password,
                        Role = nameof(UserRole.Patient)
                    };

                    this._clinicDbContext.Users.Remove(oldUser);
                    this._clinicDbContext.Patients.Add(newPatient);
                    break;

                case "Doctor":
                    var newDoctor = new Doctor
                    {
                        Id = oldUser.Id,
                        AvatarUrl = oldUser.AvatarUrl,
                        FirstName = oldUser.FirstName,
                        LastName = oldUser.LastName,
                        Address = oldUser.Address,
                        PhoneNumber = oldUser.PhoneNumber,
                        Gender = oldUser.Gender,
                        DateOfBirth = oldUser.DateOfBirth,
                        Email = oldUser.Email,
                        Password = oldUser.Password,
                        Department = this._clinicDbContext.Departments.First(),
                        DepartmentId = this._clinicDbContext.Departments.First().Id,
                        Specialization = this._clinicDbContext.Specializations.First(),
                        SpecializationId = this._clinicDbContext.Specializations.First().Id,
                        Role = nameof(UserRole.Doctor)
                    };

                    this._clinicDbContext.Users.Remove(oldUser);
                    this._clinicDbContext.Doctors.Add(newDoctor);
                    break;
                default:

                    if (oldUser.Role == nameof(UserRole.Patient))
                    {
                        var patientToDelete = await this._clinicDbContext.Patients.FirstOrDefaultAsync(p => p.Id == oldUser.Id);
                        if (patientToDelete != null)
                            this._clinicDbContext.Patients.Remove(patientToDelete);
                    }
                    else if (oldUser.Role == nameof(UserRole.Doctor))
                    {
                        var doctorToDelete = await this._clinicDbContext.Doctors.FirstOrDefaultAsync(d => d.Id == oldUser.Id);
                        if (doctorToDelete != null)
                            this._clinicDbContext.Doctors.Remove(doctorToDelete);
                    }
                    oldUser.Role = nameof(UserRole.Admin);
                    this._clinicDbContext.Update(oldUser);
                    break;
            }
        }
        else
        {
            oldUser.Role = newUser.Role ?? oldUser.Role;
            oldUser.DateOfBirth = newUser.DateOfBirth != default ? newUser.DateOfBirth : oldUser.DateOfBirth;
            this._clinicDbContext.Update(oldUser);
        }

        await this._clinicDbContext.SaveChangesAsync();
    }
}

