using MediClubApp.Models;

namespace MediClubApp.Services.Base;

public interface IDoctorService
{
    Task<IEnumerable<Doctor>> GetAllDoctorsAsync();

    Task CreateDoctorAsync(Doctor newDoctor);
}
