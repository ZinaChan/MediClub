
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
    public async Task<IEnumerable<Log>> GetAllLogsAsync()
    {
        return await _LogRepository.GetAllAsync();
    }
    public async Task<Log?> GetLogAsync(int id)
    {
        return await this._LogRepository.GetAsync(id: id);
    }
    public async Task UpdateLogAsync(int id, Log newLog)
    {

        if (newLog is null || await this._LogRepository.GetAsync(id) is null)
        {
            throw new ArgumentNullException(nameof(newLog));
        }

        await _LogRepository.UpdateAsync(id: id, newLog: newLog);
    }
    public async Task DeleteLogByIdAsync(int id)
    {
        if (await this._LogRepository.GetAsync(id) is null)
        {
            throw new ArgumentNullException(nameof(Log));
        }

        await _LogRepository.DeleteByIdAsync(id: id);
    }
}
