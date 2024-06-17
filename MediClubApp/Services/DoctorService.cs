
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
    public async Task CreateDoctorAsync(Doctor doctor)
    {
        if (doctor is null)
        {
            throw new ArgumentNullException(nameof(doctor));
        }

        await _doctorRepository.CreateDoctorAsync(doctor);   
    }

    public Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
    {
        return _doctorRepository.GetAllDoctorsAsync();  
    }
}
