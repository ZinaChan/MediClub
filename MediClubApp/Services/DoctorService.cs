
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

        await _doctorRepository.CreateDoctorAsync(newDoctor);   
    }

    public Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
    {
        return _doctorRepository.GetAllDoctorsAsync();  
    }
}
