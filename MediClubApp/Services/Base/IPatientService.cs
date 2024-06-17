using MediClubApp.Models;

namespace MediClubApp.Services.Base;

public interface IPatientService
{
    Task<Patient?> GetPatientAsync(int id);
    Task<IEnumerable<Patient>> GetAllPatientsAsync();
    Task CreatePatientAsync(Patient newPatient);
    Task UpdatePatientAsync(int id, Patient newPatient);
    Task DeletePatientAsync(Patient oldPatient);
}
