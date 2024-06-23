using MediClubApp.Models;
namespace MediClubApp.Services.Base;

public interface IAppointmentService
{
    Task<Appointment?> GetAppointmentAsync(int id);
    Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
    Task CreateAppointmentAsync(Appointment newAppointment);
    Task UpdateAppointmentAsync(int id, Appointment newAppointment);
    Task DeleteAppointmentByIdAsync(int id);
}
