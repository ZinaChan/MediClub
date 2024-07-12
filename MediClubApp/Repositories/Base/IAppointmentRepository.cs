#pragma warning disable CS0108

using MediClubApp.Models;
using MediClubApp.Repositories.Base.CRUD;

namespace MediClubApp.Repositories.Base;

public interface IAppointmentRepository :  ICreateAsync<Appointment>, IGetAsync<Appointment>, IGetAllAsync<Appointment>, IUpdateAsync<Appointment>, IDeleteAsync<Appointment>
{
    Task<Appointment?> GetAsync(Guid id);
    Task<IEnumerable<Appointment>> GetAllAsync();
    Task CreateAsync(Appointment newAppointment);
    Task UpdateAsync(Guid id, Appointment newAppointment);
    Task DeleteByIdAsync(Guid id);
}
