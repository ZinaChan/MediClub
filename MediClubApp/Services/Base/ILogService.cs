using MediClubApp.Models;
namespace MediClubApp.Services.Base;

public interface ILogService
{
    Task<Log?> GetLogAsync(Guid id);
    Task<IEnumerable<Log>> GetAllLogsAsync();
    Task CreateLogAsync(Log newLog);
    Task UpdateLogAsync(Guid id, Log newLog);
    Task DeleteLogByIdAsync(Guid id);
}
