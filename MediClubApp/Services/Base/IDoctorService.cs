using MediClubApp.Models;
namespace MediClubApp.Services.Base;

public interface IDoctorService
{
    Task<Doctor?> GetDoctorAsync(Guid id);
    Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
    Task CreateDoctorAsync(Doctor newDoctor);
    Task UpdateDoctorAsync(Guid id, Doctor newDoctor);
    Task DeleteDoctorByIdAsync(Guid id);
}
