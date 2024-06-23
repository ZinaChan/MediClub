using MediClubApp.Models;
namespace MediClubApp.Services.Base;

public interface IMedicalRecordService
{
    Task<MedicalRecord?> GetMedicalRecordAsync(int id);
    Task<IEnumerable<MedicalRecord>> GetAllMedicalRecordsAsync();
    Task CreateMedicalRecordAsync(MedicalRecord newMedicalRecord);
    Task UpdateMedicalRecordAsync(int id, MedicalRecord newMedicalRecord);
    Task DeleteMedicalRecordByIdAsync(int id);
}