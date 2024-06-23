#pragma warning disable CS0108

using MediClubApp.Models;
using MediClubApp.Repositories.Base.CRUD;

namespace MediClubApp.Repositories.Base;

public interface IDoctorRepository :  ICreateAsync<Doctor>, IGetAsync<Doctor>, IGetAllAsync<Doctor>, IUpdateAsync<Doctor>, IDeleteAsync<Doctor>
{
    Task<Doctor?> GetAsync(int id);
    Task<IEnumerable<Doctor>> GetAllAsync();
    Task CreateAsync(Doctor newDoctor);
    Task UpdateAsync(int id, Doctor newDoctor);
    Task DeleteByIdAsync(int id);
} 