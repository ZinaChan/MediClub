#pragma warning disable CS0108

using MediClubApp.Models;
using MediClubApp.Repositories.Base.CRUD;

namespace MediClubApp.Repositories.Base;

public interface ISpecializationRepository :  ICreateAsync<Specialization>, IGetAsync<Specialization>, IGetAllAsync<Specialization>, IUpdateAsync<Specialization>, IDeleteAsync<Specialization>
{
    Task<Specialization?> GetAsync(Guid id);
    Task<IEnumerable<Specialization>> GetAllAsync();
    Task CreateAsync(Specialization newSpecialization);
    Task UpdateAsync(Guid id, Specialization newSpecialization);
    Task DeleteByIdAsync(Guid id);
}