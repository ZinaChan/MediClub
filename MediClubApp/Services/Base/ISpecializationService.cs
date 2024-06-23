using MediClubApp.Models;
namespace MediClubApp.Services.Base;

public interface ISpecializationService
{
    Task<Specialization?> GetSpecializationAsync(int id);
    Task<IEnumerable<Specialization>> GetAllSpecializationsAsync();
    Task CreateSpecializationAsync(Specialization newSpecialization);
    Task UpdateSpecializationAsync(int id, Specialization newSpecialization);
    Task DeleteSpecializationByIdAsync(int id);
}