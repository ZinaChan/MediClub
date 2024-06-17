
using MediClubApp.Models;
using MediClubApp.Repositories.Base;
using MediClubApp.Services.Base;

namespace MediClubApp.Services;

public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _doctorRepository;

    public DoctorService(IDoctorRepository doctorRepository)
    {
        this._doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
    }
    public async Task CreateDoctorAsync(Doctor newDoctor)
    {
        if (newDoctor is null)
        {
            throw new ArgumentNullException(nameof(newDoctor));
        }

        await _doctorRepository.CreateAsync(newDoctor);
    }
    public Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
    {
        return _doctorRepository.GetAllAsync();
    }
    public Task<Doctor?> GetDoctorAsync(int id)
    {
        return this._doctorRepository.GetAsync(id);
    }
    public async Task UpdateDoctorAsync(int id, Doctor newDoctor)
    {

        if (newDoctor is null || this._doctorRepository.GetAsync(id) is null)
        {
            throw new ArgumentNullException(nameof(newDoctor));
        }

        await _doctorRepository.UpdateAsync(id, newDoctor);
    }
    public async Task DeleteDoctorAsync(Doctor oldDoctor)
    {
        if (oldDoctor is null)
        {
            throw new ArgumentNullException(nameof(oldDoctor));
        }

        await _doctorRepository.DeleteAsync(oldDoctor);
    }
}
