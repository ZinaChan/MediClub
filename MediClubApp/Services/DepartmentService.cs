using MediClubApp.Models;
using MediClubApp.Repositories.Base;
using MediClubApp.Services.Base;

namespace MediClubApp.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentService(IDepartmentRepository departmentRepository)
    {
        this._departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
    }
    public async Task CreateDepartmentAsync(Department newDepartment)
    {
        if (newDepartment is null)
        {
            throw new ArgumentNullException(nameof(newDepartment));
        }

        await _departmentRepository.CreateAsync(newDepartment: newDepartment);
    }
    public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
    {
        return await _departmentRepository.GetAllAsync();
    }
    public async Task<Department?> GetDepartmentAsync(Guid id)
    {
        return await this._departmentRepository.GetAsync(id: id);
    }
    public async Task UpdateDepartmentAsync(Guid id, Department newDepartment)
    {

        if (newDepartment is null || await this._departmentRepository.GetAsync(id) is null)
        {
            throw new ArgumentNullException(nameof(newDepartment));
        }

        await _departmentRepository.UpdateAsync(id: id, newDepartment: newDepartment);
    }
    public async Task DeleteDepartmentByIdAsync(Guid id)
    {
        if (await this._departmentRepository.GetAsync(id) is null)
        {
            throw new ArgumentNullException(nameof(Department));
        }

        await _departmentRepository.DeleteByIdAsync(id: id);
    }
}
