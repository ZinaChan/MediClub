#pragma warning disable CS8618

namespace MediClubApp.Models;

public class Appointment
{
    public int AppointmentId { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime DateTime { get; set; }
    public string Notes { get; set; }
}
