#pragma warning disable CS1998

using MediClubApp.Models;
using MediClubApp.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using MediClubApp.Repositories.EFCore.Dbcontext;

namespace MediClubApp.Repositories.EFCore;

public class PatientEFCoreRRepository : IPatientRepository
{
    private readonly MyClinicDbContext _clinicDbContext;
    public PatientEFCoreRRepository(MyClinicDbContext dbContext)
    {
        this._clinicDbContext = dbContext;
    }

    public async Task CreateAsync(Patient newPatient)
    {
        newPatient.Id = Guid.NewGuid();
        await this._clinicDbContext.Patients.AddAsync(entity: newPatient);
        await this._clinicDbContext.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var oldPatient = await this._clinicDbContext.Patients.FirstOrDefaultAsync(d => d.Id == id);

        if (oldPatient is null) return;

        this._clinicDbContext.Patients.Remove(entity: oldPatient);
        await this._clinicDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Patient>> GetAllAsync()
    {
        var patients = await this._clinicDbContext.Patients
                        .Include(p => p.Appointments)
                        .Include(p => p.MedicalRecords)
                        .ToListAsync();
        return patients;
    }

    public Task<Patient?> GetAsync(Guid id)
    {
        return this._clinicDbContext.Patients.FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task UpdateAsync(Guid id, Patient newPatient)
    {
        var oldPatient = await this._clinicDbContext.Patients.FirstOrDefaultAsync(d => d.Id == id);

        if (oldPatient is null) return;

        oldPatient.FirstName = newPatient.FirstName ?? oldPatient.FirstName;
        oldPatient.Gender = newPatient.Gender ?? oldPatient.Gender;
        oldPatient.Email = newPatient.Email ?? oldPatient.Email;
        oldPatient.PhoneNumber = newPatient.PhoneNumber ?? oldPatient.PhoneNumber;
        oldPatient.Address = newPatient.Address ?? oldPatient.Address;
        oldPatient.LastName = newPatient.LastName ?? oldPatient.LastName;

        if (newPatient.DateOfBirth != default(DateTime))
        {
            oldPatient.DateOfBirth = oldPatient.DateOfBirth;
        }

        this._clinicDbContext.Update(oldPatient);
        await this._clinicDbContext.SaveChangesAsync();
    }
}
