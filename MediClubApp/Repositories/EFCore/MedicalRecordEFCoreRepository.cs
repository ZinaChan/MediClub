#pragma warning disable CS1998

using MediClubApp.Models;
using MediClubApp.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using MediClubApp.Repositories.EFCore.Dbcontext;

namespace MediClubApp.Repositories.EFCore;

public class MedicalRecordEFCoreRepository : IMedicalRecordRepository
{
    private readonly MyClinicDbContext _clinicDbContext;
    public MedicalRecordEFCoreRepository(MyClinicDbContext dbContext)
    {
        this._clinicDbContext = dbContext;
    }

    public async Task CreateAsync(MedicalRecord newMedicalRecord)
    {
        await this._clinicDbContext.MedicalRecords.AddAsync(entity: newMedicalRecord);
        await this._clinicDbContext.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(int id)
    {
        var oldMedicalRecord = await this._clinicDbContext.MedicalRecords.FirstOrDefaultAsync(d => d.Id == id);

        if (oldMedicalRecord is null) return;

        this._clinicDbContext.MedicalRecords.Remove(entity: oldMedicalRecord);
        await this._clinicDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<MedicalRecord>> GetAllAsync()
    {
        var medicalRecords = await this._clinicDbContext.MedicalRecords
                                .Include(m => m.Doctor)
                                .Include(m => m.Patient)
                                .ToListAsync();
        return medicalRecords;
    }

    public Task<MedicalRecord?> GetAsync(int id)
    {
        return this._clinicDbContext.MedicalRecords.FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task UpdateAsync(int id, MedicalRecord newMedicalRecord)
    {
        var oldMedicalRecord = await this._clinicDbContext.MedicalRecords.FirstOrDefaultAsync(d => d.Id == id);
        if (oldMedicalRecord is null) return;

        oldMedicalRecord.PatientId = newMedicalRecord.PatientId;
        oldMedicalRecord.DoctorId = newMedicalRecord.DoctorId;
        oldMedicalRecord.Date = newMedicalRecord.Date;
        oldMedicalRecord.Diagnosis = newMedicalRecord.Diagnosis;
        oldMedicalRecord.Treatment = newMedicalRecord.Treatment;

        this._clinicDbContext.Update(oldMedicalRecord);
        await this._clinicDbContext.SaveChangesAsync();
    }
}
