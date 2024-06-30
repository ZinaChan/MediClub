using MediClubApp.Models;
using MediClubApp.Repositories.Base;
using MediClubApp.Services.Base;

namespace MediClubApp.Services;

public class MedicalRecordService : IMedicalRecordService
{
    private readonly IMedicalRecordRepository _medicalRecordRepository;

    public MedicalRecordService(IMedicalRecordRepository medicalRecordRepository)
    {
        this._medicalRecordRepository = medicalRecordRepository ?? throw new ArgumentNullException(nameof(medicalRecordRepository));
    }
    public async Task CreateMedicalRecordAsync(MedicalRecord newMedicalRecord)
    {
        if (newMedicalRecord is null)
        {
            throw new ArgumentNullException(nameof(newMedicalRecord));
        }

        await _medicalRecordRepository.CreateAsync(newMedicalRecord: newMedicalRecord);
    }
    public async Task<IEnumerable<MedicalRecord>> GetAllMedicalRecordsAsync()
    {
        return await _medicalRecordRepository.GetAllAsync();
    }
    public async Task<MedicalRecord?> GetMedicalRecordAsync(Guid id)
    {
        return await this._medicalRecordRepository.GetAsync(id: id);
    }
    public async Task UpdateMedicalRecordAsync(Guid id, MedicalRecord newMedicalRecord)
    {

        if (newMedicalRecord is null || await this._medicalRecordRepository.GetAsync(id) is null)
        {
            throw new ArgumentNullException(nameof(newMedicalRecord));
        }

        await _medicalRecordRepository.UpdateAsync(id: id, newMedicalRecord: newMedicalRecord);
    }
    public async Task DeleteMedicalRecordByIdAsync(Guid id)
    {
        if (await this._medicalRecordRepository.GetAsync(id) is null)
        {
            throw new ArgumentNullException(nameof(MedicalRecord));
        }

        await _medicalRecordRepository.DeleteByIdAsync(id: id);
    }
}

