#pragma warning disable CS0108

using MediClubApp.Models;
using MediClubApp.Repositories.Base.CRUD;

namespace MediClubApp.Repositories.Base;

public interface IMedicalRecordRepository :  ICreateAsync<MedicalRecord>, IGetAsync<MedicalRecord>, IGetAllAsync<MedicalRecord>, IUpdateAsync<MedicalRecord>, IDeleteAsync<MedicalRecord>
{
    Task<MedicalRecord?> GetAsync(Guid id);
    Task<IEnumerable<MedicalRecord>> GetAllAsync();
    Task CreateAsync(MedicalRecord newMedicalRecord);
    Task UpdateAsync(Guid id, MedicalRecord newMedicalRecord);
    Task DeleteByIdAsync(Guid id);

     Task<IEnumerable<MedicalRecord>> GetMedicalRecordsForPatientAsync(Guid patientId);

     Task<IEnumerable<MedicalRecord>> GetMedicalRecordsForDoctorAsync(Guid doctorId);
}
