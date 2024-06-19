#pragma warning disable CS0108

using MediClubApp.Models;

namespace MediClubApp.Repositories.Base;

public interface IPatientRepository : ICreateAsync<Patient>, IGetAsync<Patient>, IGetAllAsync<Patient>, IUpdateAsync<Patient>, IDeleteAsync<Patient>
{
    Task<Patient?> GetAsync(int id);
    Task<IEnumerable<Patient>> GetAllAsync();
    Task CreateAsync(Patient newPatient);
    Task UpdateAsync(int id, Patient newPatient);
    Task DeleteByIdAsync(int id);
}
