#pragma warning disable CS8618

namespace MediClubApp.Models;

public class Patient
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    public ICollection<Appointment> Appointments { get; set; }
    public ICollection<MedicalRecord> MedicalRecords { get; set; }
}
