#pragma warning disable CS1998

using MediClubApp.Models;
using MediClubApp.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using MediClubApp.Repositories.EFCore.Dbcontext;

namespace MediClubApp.Repositories.EFCore;

public class DepartmentEFCoreRepository : IDepartmentRepository
{
    private readonly MyClinicDbContext _clinicDbContext;
    public DepartmentEFCoreRepository(MyClinicDbContext dbContext)
    {
        this._clinicDbContext = dbContext;
    }

    public async Task CreateAsync(Department newDepartment)
    {
        await this._clinicDbContext.Departments.AddAsync(entity: newDepartment);
        await this._clinicDbContext.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(int id)
    {
        var oldDepartment = await this._clinicDbContext.Departments.FirstOrDefaultAsync(d => d.Id == id);

        if (oldDepartment is null) return;

        this._clinicDbContext.Departments.Remove(entity: oldDepartment);
        await this._clinicDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Department>> GetAllAsync()
    {
        var departments = await this._clinicDbContext.Departments
                        .Include(d => d.Doctors)
                        .Include(d => d.Rooms)
                        .ToListAsync();
        return departments;  
    }

    public Task<Department?> GetAsync(int id)
    {
        return this._clinicDbContext.Departments.FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task UpdateAsync(int id, Department newDepartment)
    {
        var oldDepartment = await this._clinicDbContext.Departments.FirstOrDefaultAsync(d => d.Id == id);
        if (oldDepartment is null) return;

        oldDepartment.Name = newDepartment.Name;

        this._clinicDbContext.Update(oldDepartment);
        await this._clinicDbContext.SaveChangesAsync();
    }
}

