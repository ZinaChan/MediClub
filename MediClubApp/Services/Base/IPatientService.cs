using MediClubApp.Models;

namespace MediClubApp.Services.Base;

public interface IPatientService
{
    Task<IEnumerable<Patient>> GetAllPatientsAsync();

    Task CreatePatientAsync(Patient newPatient);
}
