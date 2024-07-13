using MediClubApp.Models;
namespace MediClubApp.Services.Base;

public interface IPatientService
{
    Task<Patient?> GetPatientAsync(Guid id);
    Task<IEnumerable<Patient>> GetAllPatientsAsync();
    Task CreatePatientAsync(Patient newPatient);
    Task UpdatePatientAsync(Guid id, Patient newPatient);
    Task DeletePatientByIdAsync(Guid id);
    Task<IEnumerable<Patient>> GetPatientsByDoctorAsync(Guid doctorId);
}
