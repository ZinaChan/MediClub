using MediClubApp.Models;
namespace MediClubApp.Services.Base;

public interface IDepartmentService
{
    Task<Department?> GetDepartmentAsync(Guid id);
    Task<IEnumerable<Department>> GetAllDepartmentsAsync();
    Task CreateDepartmentAsync(Department newDepartment);
    Task UpdateDepartmentAsync(Guid id, Department newDepartment);
    Task DeleteDepartmentByIdAsync(Guid id);
}
