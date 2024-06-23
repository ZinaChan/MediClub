#pragma warning disable CS8618
using MediClubApp.Models;

namespace MediClubApp.ViewModels;

public class MedicalRecordViewModel
{
    public MedicalRecord MedicalRecord { get; set; }
    public IEnumerable<Patient> Patients { get; set; }
    public IEnumerable<Doctor> Doctors { get; set; }
    public IEnumerable<MedicalRecord> MedicalRecords { get; set; }
}