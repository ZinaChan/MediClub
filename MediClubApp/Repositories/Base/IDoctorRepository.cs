using MediClubApp.Models;

namespace MediClubApp.Repositories.Base;

public interface IDoctorRepository :  ICreateAsync<Doctor>, IGetAsync<Doctor>, IGetAllAsync<Doctor>, IUpdateAsync<Doctor>, IDeleteAsync<Doctor>
{
    Task<Doctor?> GetAsync(int id);
    Task<IEnumerable<Doctor>> GetAllAsync();
    Task CreateAsync(Doctor newDoctor);
    Task UpdateAsync(int id, Doctor newDoctor);
    Task DeleteAsync(Doctor oldDoctor);
}
