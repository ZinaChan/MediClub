#pragma warning disable CS8618
using MediClubApp.Models;

namespace MediClubApp.ViewModels;
public class AppointmentViewModel
{
    public Appointment Appointment { get; set; }
    public IEnumerable<Patient> Patients { get; set; }
    public IEnumerable<Doctor> Doctors { get; set; }
    public IEnumerable<Room> Rooms { get; set; } 
    public IEnumerable<Appointment> Appointments { get; set; } 
}