#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediClubApp.Models;

public class Doctor : User
{  
    public Guid DepartmentId { get; set; }

    [ForeignKey("DepartmentId")]
    public Department Department { get; set; }
    public Guid SpecializationId { get; set; }

    [ForeignKey("SpecializationId")]
    public Specialization Specialization { get; set; }

    public ICollection<Appointment> Appointments { get; set; }
    public ICollection<MedicalRecord> MedicalRecords { get; set; }
}
