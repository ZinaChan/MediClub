#pragma warning disable CS0108

using MediClubApp.Models;
using MediClubApp.Repositories.Base.CRUD;

namespace MediClubApp.Repositories.Base;

public interface IMedicalRecordRepository :  ICreateAsync<MedicalRecord>, IGetAsync<MedicalRecord>, IGetAllAsync<MedicalRecord>, IUpdateAsync<MedicalRecord>, IDeleteAsync<MedicalRecord>
{
    Task<MedicalRecord?> GetAsync(int id);
    Task<IEnumerable<MedicalRecord>> GetAllAsync();
    Task CreateAsync(MedicalRecord newMedicalRecord);
    Task UpdateAsync(int id, MedicalRecord newMedicalRecord);
    Task DeleteByIdAsync(int id);
}
