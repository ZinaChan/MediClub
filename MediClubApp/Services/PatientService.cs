
using MediClubApp.Models;
using MediClubApp.Repositories.Base;
using MediClubApp.Services.Base;

namespace MediClubApp.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;

    public PatientService(IPatientRepository patientRepository)
    {
       this._patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository)); 
    }
    public async Task CreatePatientAsync(Patient patient)
    {
        if (patient is null)
        {
            throw new ArgumentNullException(nameof(patient));
        }

        await this._patientRepository.CreatePatientAsync(patient);
    }

    public Task<IEnumerable<Patient>> GetAllPatientsAsync()
    {
        return this._patientRepository.GetAllPatientsAsync();
    }
}
