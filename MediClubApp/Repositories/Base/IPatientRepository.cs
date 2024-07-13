#pragma warning disable CS0108

using MediClubApp.Models;
using MediClubApp.Repositories.Base.CRUD;

namespace MediClubApp.Repositories.Base;

public interface IPatientRepository : ICreateAsync<Patient>, IGetAsync<Patient>, IGetAllAsync<Patient>, IUpdateAsync<Patient>, IDeleteAsync<Patient>
{
    Task<Patient?> GetAsync(Guid id);
    Task<IEnumerable<Patient>> GetAllAsync();
    Task CreateAsync(Patient newPatient);
    Task UpdateAsync(Guid id, Patient newPatient);
    Task DeleteByIdAsync(Guid id);
    Task<IEnumerable<Patient>> GetPatientsByDoctorAsync(Guid doctorId);
}
