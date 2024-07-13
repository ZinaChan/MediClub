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
    public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
    {
        return await this._patientRepository.GetAllAsync();
    }
    public async Task<Patient?> GetPatientAsync(Guid id)
    {
        return await this._patientRepository.GetAsync(id: id);
    }
    public async Task UpdatePatientAsync(Guid id, Patient newPatient)
    {
        if (newPatient is null || await this._patientRepository.GetAsync(id) is null)
        {
            throw new ArgumentNullException(nameof(newPatient));
        }

        await this._patientRepository.UpdateAsync(id: id, newPatient: newPatient);
    }
    public async Task DeletePatientByIdAsync(Guid id)
    {
        if (await this._patientRepository.GetAsync(id) is null)
        {
            throw new ArgumentNullException(nameof(Patient));
        }

        await this._patientRepository.DeleteByIdAsync(id: id);
    }

    public async Task<IEnumerable<Patient>> GetPatientsByDoctorAsync(Guid doctorId)
    { 
       return await this._patientRepository.GetPatientsByDoctorAsync(doctorId: doctorId);
    }
}
