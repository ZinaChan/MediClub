#pragma warning disable CS0108

using MediClubApp.Models;
using MediClubApp.Repositories.Base.CRUD;

namespace MediClubApp.Repositories.Base;

public interface IDepartmentRepository :  ICreateAsync<Department>, IGetAsync<Department>, IGetAllAsync<Department>, IUpdateAsync<Department>, IDeleteAsync<Department>
{
    Task<Department?> GetAsync(Guid id);
    Task<IEnumerable<Department>> GetAllAsync();
    Task CreateAsync(Department newDepartment);
    Task UpdateAsync(Guid id, Department newDepartment);
    Task DeleteByIdAsync(Guid id);
}
