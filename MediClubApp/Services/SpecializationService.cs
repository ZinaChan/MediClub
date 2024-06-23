using MediClubApp.Models;
using MediClubApp.Repositories.Base;
using MediClubApp.Services.Base;

namespace MediClubApp.Services;

public class SpecializationService : ISpecializationService
{
    private readonly ISpecializationRepository _specializationRepository;

    public SpecializationService(ISpecializationRepository specializationRepository)
    {
        this._specializationRepository = specializationRepository ?? throw new ArgumentNullException(nameof(specializationRepository));
    }
    public async Task CreateSpecializationAsync(Specialization newSpecialization)
    {
        if (newSpecialization is null)
        {
            throw new ArgumentNullException(nameof(newSpecialization));
        }

        await _specializationRepository.CreateAsync(newSpecialization: newSpecialization);
    }
    public async Task<IEnumerable<Specialization>> GetAllSpecializationsAsync()
    {
        return await _specializationRepository.GetAllAsync();
    }
    public async Task<Specialization?> GetSpecializationAsync(int id)
    {
        return await this._specializationRepository.GetAsync(id: id);
    }
    public async Task UpdateSpecializationAsync(int id, Specialization newSpecialization)
    {

        if (newSpecialization is null || await this._specializationRepository.GetAsync(id) is null)
        {
            throw new ArgumentNullException(nameof(newSpecialization));
        }

        await _specializationRepository.UpdateAsync(id: id, newSpecialization: newSpecialization);
    }
    public async Task DeleteSpecializationByIdAsync(int id)
    {
        if (await this._specializationRepository.GetAsync(id) is null)
        {
            throw new ArgumentNullException(nameof(Specialization));
        }

        await _specializationRepository.DeleteByIdAsync(id: id);
    }
}
