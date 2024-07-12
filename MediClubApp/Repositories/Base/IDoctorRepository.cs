#pragma warning disable CS0108

using MediClubApp.Models;
using MediClubApp.Repositories.Base.CRUD;

namespace MediClubApp.Repositories.Base;

public interface IDoctorRepository :  ICreateAsync<Doctor>, IGetAsync<Doctor>, IGetAllAsync<Doctor>, IUpdateAsync<Doctor>, IDeleteAsync<Doctor>
{
    Task<Doctor?> GetAsync(Guid id);
    Task<IEnumerable<Doctor>> GetAllAsync();
    Task CreateAsync(Doctor newDoctor);
    Task UpdateAsync(Guid id, Doctor newDoctor);
    Task DeleteByIdAsync(Guid id);
} 