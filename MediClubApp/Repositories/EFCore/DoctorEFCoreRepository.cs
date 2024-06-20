#pragma warning disable CS1998

using MediClubApp.Models;
using MediClubApp.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using MediClubApp.Repositories.EFCore.Dbcontext;

namespace MediClubApp.Repositories.EFCore;

public class DoctorEFCoreRepository : IDoctorRepository
{
    private readonly MyClinicDbContext _clinicDbContext;
    public DoctorEFCoreRepository(MyClinicDbContext dbContext)
    {
        this._clinicDbContext = dbContext;
    }

    public async Task CreateAsync(Doctor newDoctor)
    {
        await this._clinicDbContext.Doctors.AddAsync(entity: newDoctor);
        await this._clinicDbContext.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(int id)
    {
        var oldDoctor = await this._clinicDbContext.Doctors.FirstOrDefaultAsync(d => d.Id == id);

        if (oldDoctor is null) return;

        this._clinicDbContext.Doctors.Remove(entity: oldDoctor);
        await this._clinicDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Doctor>> GetAllAsync()
    {
        return this._clinicDbContext.Doctors;
    }

    public Task<Doctor?> GetAsync(int id)
    {
        return this._clinicDbContext.Doctors.FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task UpdateAsync(int id, Doctor newDoctor)
    {
        var oldDoctor = await this._clinicDbContext.Doctors.FirstOrDefaultAsync(d => d.Id == id);
        if (oldDoctor is null) return;

        oldDoctor.FirstName = newDoctor.FirstName;
        oldDoctor.LastName = newDoctor.LastName;
        oldDoctor.DateOfBirth = newDoctor.DateOfBirth;
        oldDoctor.Gender = newDoctor.Gender;
        oldDoctor.Email = newDoctor.Email;
        oldDoctor.PhoneNumber = newDoctor.PhoneNumber;
        oldDoctor.Specialization = newDoctor.Specialization;
        oldDoctor.Department = newDoctor.Department;

        this._clinicDbContext.Update(oldDoctor);
        await this._clinicDbContext.SaveChangesAsync();
    }
}
