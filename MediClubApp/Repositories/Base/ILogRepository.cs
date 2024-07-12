#pragma warning disable CS0108

using MediClubApp.Models;
using MediClubApp.Repositories.Base.CRUD;

namespace MediClubApp.Repositories.Base;

public interface ILogRepository : ICreateAsync<Log>, IGetAsync<Log>, IGetAllAsync<Log>, IUpdateAsync<Log>, IDeleteAsync<Log>
{
    Task<Log?> GetAsync(Guid id);
    Task<IEnumerable<Log>> GetAllAsync();
    Task CreateAsync(Log newLog);
    Task UpdateAsync(Guid id, Log newLog);
    Task DeleteByIdAsync(Guid id);
}
