#pragma warning disable CS1998

using MediClubApp.Models;
using MediClubApp.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using MediClubApp.Repositories.EFCore.Dbcontext;

namespace MediClubApp.Repositories.EFCore;

public class AppointmentEFCoreRepository : IAppointmentRepository
{
    private readonly MyClinicDbContext _clinicDbContext;
    public AppointmentEFCoreRepository(MyClinicDbContext dbContext)
    {
        this._clinicDbContext = dbContext;
    }

    public async Task CreateAsync(Appointment newAppointment)
    {
        newAppointment.Id = Guid.NewGuid();
        await this._clinicDbContext.Appointments.AddAsync(entity: newAppointment);
        await this._clinicDbContext.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var oldAppointment = await this._clinicDbContext.Appointments.FirstOrDefaultAsync(d => d.Id == id);

        if (oldAppointment is null) return;

        this._clinicDbContext.Appointments.Remove(entity: oldAppointment);
        await this._clinicDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Appointment>> GetAllAsync()
    {
        var appointment = await this._clinicDbContext.Appointments
                        .Include(a => a.Doctor)
                        .Include(a => a.Patient)
                        .Include(a => a.Room)
                        .ToListAsync();
        return appointment;
    }

    public async Task<IEnumerable<Appointment>> GetAppointmentsForDoctorAsync(Guid doctorId)
    {
        var appointments = await _clinicDbContext.Appointments
                            .Include(a => a.Doctor)
                            .Include(a => a.Patient)
                            .Include(a => a.Room)
                            .Where(a => a.DoctorId == doctorId)
                            .ToListAsync();

        return appointments;
    }

    public async Task<IEnumerable<Appointment>> GetAppointmentsForPatientAsync(Guid patientId)
    {
        var appointments = await _clinicDbContext.Appointments
                            .Include(a => a.Doctor)
                            .Include(a => a.Patient)
                            .Include(a => a.Room)
                            .Where(a => a.PatientId == patientId)
                            .ToListAsync();

        return appointments;
    }

    public Task<Appointment?> GetAsync(Guid id)
    {
        return this._clinicDbContext.Appointments.FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task UpdateAsync(Guid id, Appointment newAppointment)
    {
        var oldAppointment = await this._clinicDbContext.Appointments.FirstOrDefaultAsync(d => d.Id == id);
        if (oldAppointment is null) return;

        oldAppointment.PatientId = newAppointment.PatientId;
        oldAppointment.Patient = await this._clinicDbContext.Patients.FirstOrDefaultAsync(s => s.Id == newAppointment.PatientId) ?? new Patient();
        oldAppointment.DoctorId = newAppointment.DoctorId;
        oldAppointment.Doctor = await this._clinicDbContext.Doctors.FirstOrDefaultAsync(s => s.Id == newAppointment.DoctorId) ?? new Doctor();
        oldAppointment.RoomId = newAppointment.RoomId;
        oldAppointment.Room = await this._clinicDbContext.Rooms.FirstOrDefaultAsync(s => s.Id == newAppointment.RoomId) ?? new Room();
        oldAppointment.Reason = newAppointment.Reason ?? oldAppointment.Reason;

        if (newAppointment.Date != default(DateTime))
        {
            oldAppointment.Date = newAppointment.Date;
        }
        if (newAppointment.Time != default(TimeSpan))
        {
            oldAppointment.Time = newAppointment.Time;
        }

        this._clinicDbContext.Update(oldAppointment);
        await this._clinicDbContext.SaveChangesAsync();
    }

    public async Task<Appointment> GetOverlappingAppointmentAsync(Guid doctorId, Guid roomId, DateTime date, TimeSpan time)
    {
        return await _clinicDbContext.Appointments.FirstOrDefaultAsync(a => a.Date == date && a.Time == time && (a.DoctorId == doctorId  || a.RoomId == roomId ));
    }
}