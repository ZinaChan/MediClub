
using MediClubApp.Models;
using MediClubApp.Repositories.Base;
using MediClubApp.Services.Base;

namespace MediClubApp.Services;

public class LogService : ILogService
{
    private readonly ILogRepository _LogRepository;

    public LogService(ILogRepository LogRepository)
    {
        this._LogRepository = LogRepository ?? throw new ArgumentNullException(nameof(LogRepository));
    }
    public async Task CreateLogAsync(Log newLog)
    {
        if (newLog is null)
        {
            throw new ArgumentNullException(nameof(newLog));
        }

        await _LogRepository.CreateAsync(newLog: newLog);
    }
    public Task<IEnumerable<Log>> GetAllLogsAsync()
    {
        return _LogRepository.GetAllAsync();
    }
    public Task<Log?> GetLogAsync(int id)
    {
        return this._LogRepository.GetAsync(id: id);
    }
    public async Task UpdateLogAsync(int id, Log newLog)
    {

        if (newLog is null || this._LogRepository.GetAsync(id) is null)
        {
            throw new ArgumentNullException(nameof(newLog));
        }

        await _LogRepository.UpdateAsync(id: id, newLog: newLog);
    }
    public async Task DeleteLogByIdAsync(int id)
    {
        if (this._LogRepository.GetAsync(id) is null)
        {
            throw new ArgumentNullException(nameof(Log));
        }

        await _LogRepository.DeleteByIdAsync(id: id);
    }
}
