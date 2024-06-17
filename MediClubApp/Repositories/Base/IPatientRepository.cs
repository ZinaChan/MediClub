using MediClubApp.Models;

namespace MediClubApp.Repositories.Base;

public interface IPatientRepository
{
    Task<IEnumerable<Patient>> GetAllPatientsAsync();

    Task CreatePatientAsync(Patient patient);
}
