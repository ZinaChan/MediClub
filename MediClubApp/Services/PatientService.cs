
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
    public async Task CreatePatientAsync(Patient newPatient)
    {
        if (newPatient is null)
        {
            throw new ArgumentNullException(nameof(newPatient));
        }

        await this._patientRepository.CreateAsync(newPatient: newPatient);
    }
    public Task<IEnumerable<Patient>> GetAllPatientsAsync()
    {
        return this._patientRepository.GetAllAsync();
    }
    public Task<Patient?> GetPatientAsync(int id)
    {
        return this._patientRepository.GetAsync(id: id);
    }
    public async Task UpdatePatientAsync(int id, Patient newPatient)
    {
        if (newPatient is null || this._patientRepository.GetAsync(id) is null)
        {
            throw new ArgumentNullException(nameof(newPatient));
        }

        await this._patientRepository.UpdateAsync(id: id, newPatient:newPatient);
    }
    public async Task DeletePatientByIdAsync(int id)
    {
        if (this._patientRepository.GetAsync(id) is null)
        {
            throw new ArgumentNullException(nameof(Patient));
        }

        await this._patientRepository.DeleteByIdAsync(id: id);
    }

}
