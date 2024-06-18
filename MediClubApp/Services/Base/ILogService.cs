using MediClubApp.Models;

namespace MediClubApp.Services.Base;

public interface ILogService
{
    Task<Log?> GetLogAsync(int id);
    Task<IEnumerable<Log>> GetAllLogsAsync();
    Task CreateLogAsync(Log newLog);
    Task UpdateLogAsync(int id, Log newLog);
    Task DeleteLogByIdAsync(int id);
}
