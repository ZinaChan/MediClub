#pragma warning disable CS8618

namespace MediClubApp.Models;

public class Appointment
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public Patient Patient { get; set; }
    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; }
    public int RoomId { get; set; }
    public Room Room { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    public string Reason { get; set; }
}

