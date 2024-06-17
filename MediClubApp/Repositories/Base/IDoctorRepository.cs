using MediClubApp.Models;

namespace MediClubApp.Repositories.Base;

public interface IDoctorRepository
{
    Task<IEnumerable<Doctor>> GetAllDoctorsAsync();

    Task CreateDoctorAsync(Doctor newDoctor);
}
