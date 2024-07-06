#pragma warning disable CS8618

namespace MediClubApp.Models;

public class Patient : User
{ 

    public ICollection<Appointment> Appointments { get; set; }
    public ICollection<MedicalRecord> MedicalRecords { get; set; }
}
