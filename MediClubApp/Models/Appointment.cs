#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediClubApp.Models;

public class Appointment
{
    [Key]
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }

    [ForeignKey("PatientId")]
    public Patient Patient { get; set; }
    public Guid DoctorId { get; set; }

    [ForeignKey("DoctorId")]
    public Doctor Doctor { get; set; }

    [ForeignKey("RoomId")]
    public Guid RoomId { get; set; }
    public Room Room { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    public string Reason { get; set; }
}

