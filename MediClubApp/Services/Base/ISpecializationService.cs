using MediClubApp.Models;
namespace MediClubApp.Services.Base;

public interface ISpecializationService
{
    Task<Specialization?> GetSpecializationAsync(Guid id);
    Task<IEnumerable<Specialization>> GetAllSpecializationsAsync();
    Task CreateSpecializationAsync(Specialization newSpecialization);
    Task UpdateSpecializationAsync(Guid id, Specialization newSpecialization);
    Task DeleteSpecializationByIdAsync(Guid id);
}