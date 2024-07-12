#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediClubApp.Models;

public class MedicalRecord
{
    [Key]
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }

    [ForeignKey("PatientId")]
    public Patient Patient { get; set; }
    public Guid DoctorId { get; set; }

    [ForeignKey("DoctorId")]
    public Doctor Doctor { get; set; }
    public DateTime Date { get; set; }
    public string Diagnosis { get; set; }
    public string Treatment { get; set; }
}
