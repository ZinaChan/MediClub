using MediClubApp.Models;
using MediClubApp.Repositories.Base;
using MediClubApp.Services.Base;

namespace MediClubApp.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;

    public AppointmentService(IAppointmentRepository appointmentRepository)
    {
        this._appointmentRepository = appointmentRepository ?? throw new ArgumentNullException(nameof(appointmentRepository));
    }
    public async Task CreateAppointmentAsync(Appointment newAppointment)
    {
        if (newAppointment is null)
        {
            throw new ArgumentNullException(nameof(newAppointment));
        }

        await _appointmentRepository.CreateAsync(newAppointment: newAppointment);
    }
    public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
    {
        return await _appointmentRepository.GetAllAsync();
    }
    public async Task<Appointment?> GetAppointmentAsync(Guid id)
    {
        return await this._appointmentRepository.GetAsync(id: id);
    }
    public async Task UpdateAppointmentAsync(Guid id, Appointment newAppointment)
    {

        if (newAppointment is null || await this._appointmentRepository.GetAsync(id) is null)
        {
            throw new ArgumentNullException(nameof(newAppointment));
        }

        await _appointmentRepository.UpdateAsync(id: id, newAppointment: newAppointment);
    }
    public async Task DeleteAppointmentByIdAsync(Guid id)
    {
        if (await this._appointmentRepository.GetAsync(id) is null)
        {
            throw new ArgumentNullException(nameof(Appointment));
        }

        await _appointmentRepository.DeleteByIdAsync(id: id);
    }
}

