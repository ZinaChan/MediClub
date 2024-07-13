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
        newDoctor.Id = Guid.NewGuid();
        await this._clinicDbContext.Doctors.AddAsync(entity: newDoctor);
        await this._clinicDbContext.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var oldDoctor = await this._clinicDbContext.Doctors.FirstOrDefaultAsync(d => d.Id == id);

        if (oldDoctor is null) return;

        this._clinicDbContext.Doctors.Remove(entity: oldDoctor);
        await this._clinicDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Doctor>> GetAllAsync()
    {
        var doctors = await this._clinicDbContext.Doctors
                        .Include(d => d.Department)
                        .Include(d => d.Specialization)
                        .ToListAsync();
        return doctors;
    }

    public async Task<Doctor?> GetAsync(Guid id)
    {
        return await this._clinicDbContext.Doctors.FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<List<Doctor>> GetDoctorsByDepartmentAsync(Guid departmentId)
    {
        return await this._clinicDbContext.Doctors.Where(d => d.DepartmentId == departmentId).ToListAsync();
    }

    public async Task<List<Doctor>> GetDoctorsBySpecializationAsync(Guid specializationId)
    {
        return await this._clinicDbContext.Doctors.Where(d => d.SpecializationId == specializationId).ToListAsync();
    }

    public async Task UpdateAsync(Guid id, Doctor newDoctor)
    {
        var oldDoctor = await this._clinicDbContext.Doctors.FirstOrDefaultAsync(d => d.Id == id);
        if (oldDoctor is null) return;


        oldDoctor.DepartmentId = newDoctor.DepartmentId;
        oldDoctor.Department = await this._clinicDbContext.Departments.FirstOrDefaultAsync(d => d.Id == newDoctor.DepartmentId) ?? new Department();
        oldDoctor.SpecializationId = newDoctor.SpecializationId;
        oldDoctor.Specialization = await this._clinicDbContext.Specializations.FirstOrDefaultAsync(s => s.Id == newDoctor.SpecializationId) ?? new Specialization();

        this._clinicDbContext.Update(oldDoctor);
        await this._clinicDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Doctor>> GetDoctorsByPatientAsync(Guid patientId)
    {
        var doctors = await _clinicDbContext.Doctors
               .Where(p => p.Appointments.Any(a => a.DoctorId == patientId))
               .ToListAsync();

        return doctors;
    }
}
