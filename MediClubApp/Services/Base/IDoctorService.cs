using MediClubApp.Models;
namespace MediClubApp.Services.Base;

public interface IDoctorService
{
    Task<Doctor?> GetDoctorAsync(int id);
    Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
    Task CreateDoctorAsync(Doctor newDoctor);
    Task UpdateDoctorAsync(int id, Doctor newDoctor);
    Task DeleteDoctorByIdAsync(int id);
}
