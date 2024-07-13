using MediClubApp.Models;
namespace MediClubApp.Services.Base;

public interface IAppointmentService
{
    Task<Appointment?> GetAppointmentAsync(Guid id);
    Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
    Task CreateAppointmentAsync(Appointment newAppointment);
    Task UpdateAppointmentAsync(Guid id, Appointment newAppointment);
    Task DeleteAppointmentByIdAsync(Guid id);

    Task<IEnumerable<Appointment>> GetAppointmentsForDoctorAsync(Guid doctorId);
    Task<IEnumerable<Appointment>> GetAppointmentsForPatientAsync(Guid patientId);
}
