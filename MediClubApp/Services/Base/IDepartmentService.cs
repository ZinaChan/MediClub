using MediClubApp.Models;
namespace MediClubApp.Services.Base;

public interface IDepartmentService
{
    Task<Department?> GetDepartmentAsync(int id);
    Task<IEnumerable<Department>> GetAllDepartmentsAsync();
    Task CreateDepartmentAsync(Department newDepartment);
    Task UpdateDepartmentAsync(int id, Department newDepartment);
    Task DeleteDepartmentByIdAsync(int id);
}
