#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediClubApp.Models;

public class Doctor
{
    [Key]
    public Guid Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; }

    [EmailAddress(ErrorMessage = "Invalid Email Adress.")]
    public string Email { get; set; }

    [Phone(ErrorMessage = "Invalid Phone Number.")]
    public string PhoneNumber { get; set; }
    public Guid DepartmentId { get; set; }

    [ForeignKey("DepartmentId")]
    public Department Department { get; set; }
    public Guid SpecializationId { get; set; }

    [ForeignKey("SpecializationId")]
    public Specialization Specialization { get; set; }

    public ICollection<Appointment> Appointments { get; set; }
    public ICollection<MedicalRecord> MedicalRecords { get; set; }
}
