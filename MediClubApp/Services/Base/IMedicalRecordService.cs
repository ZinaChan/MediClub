using MediClubApp.Models;
namespace MediClubApp.Services.Base;

public interface IMedicalRecordService
{
    Task<MedicalRecord?> GetMedicalRecordAsync(Guid id);
    Task<IEnumerable<MedicalRecord>> GetAllMedicalRecordsAsync();
    Task CreateMedicalRecordAsync(MedicalRecord newMedicalRecord);
    Task UpdateMedicalRecordAsync(Guid id, MedicalRecord newMedicalRecord);
    Task DeleteMedicalRecordByIdAsync(Guid id);

    Task<IEnumerable<MedicalRecord>> GetMedicalRecordsForPatientAsync(Guid patientId);
     Task<IEnumerable<MedicalRecord>> GetMedicalRecordsForDoctorAsync(Guid doctorId);
}