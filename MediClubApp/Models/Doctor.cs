#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;

namespace MediClubApp.Models;

public class Doctor
{
    public int Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; }

    [Phone(ErrorMessage = "Invalid Phone Number.")]
    public string Email { get; set; }

    [Phone(ErrorMessage = "Invalid Phone Number.")]
    public string PhoneNumber { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; }
    public int SpecializationId { get; set; }
    public Specialization Specialization { get; set; }

    public ICollection<Appointment> Appointments { get; set; }
    public ICollection<MedicalRecord> MedicalRecords { get; set; }
}
