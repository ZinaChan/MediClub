#pragma warning disable CS1998

using MediClubApp.Models;
using MediClubApp.Repositories.Base;
using MediClubApp.Repositories.EFCore.Dbcontext;
using Microsoft.EntityFrameworkCore;

namespace MediClubApp.Repositories.EFCore;

public class SpecializationEFCoreRepository : ISpecializationRepository
{
    private readonly MyClinicDbContext _clinicDbContext;
    public SpecializationEFCoreRepository(MyClinicDbContext dbContext)
    {
        this._clinicDbContext = dbContext;
    }

    public async Task CreateAsync(Specialization newSpecialization)
    {
        await this._clinicDbContext.Specializations.AddAsync(entity: newSpecialization);
        await this._clinicDbContext.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(int id)
    {
        var oldSpecialization = await this._clinicDbContext.Specializations.FirstOrDefaultAsync(d => d.Id == id);

        if (oldSpecialization is null) return;

        this._clinicDbContext.Specializations.Remove(entity: oldSpecialization);
        await this._clinicDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Specialization>> GetAllAsync()
    {
        var specializations = await this._clinicDbContext.Specializations.Include(r => r.Department).ToListAsync();

        return specializations;
    }

    public Task<Specialization?> GetAsync(int id)
    {
        return this._clinicDbContext.Specializations.FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task UpdateAsync(int id, Specialization newSpecialization)
    {
        var oldSpecialization = await this._clinicDbContext.Specializations.FirstOrDefaultAsync(d => d.Id == id);
        if (oldSpecialization is null) return;

        oldSpecialization.Name = newSpecialization.Name;
        oldSpecialization.DepartmentId = newSpecialization.DepartmentId;
        oldSpecialization.Department = await this._clinicDbContext.Departments.FirstOrDefaultAsync(p => p.Id == oldSpecialization.DepartmentId) ?? new Department();

        this._clinicDbContext.Update(oldSpecialization);
        await this._clinicDbContext.SaveChangesAsync();
    }
}
