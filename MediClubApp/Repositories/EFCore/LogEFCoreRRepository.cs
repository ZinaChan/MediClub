#pragma warning disable CS1998
using MediClubApp.Models;
using MediClubApp.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using MediClubApp.Repositories.EFCore.Dbcontext;

namespace MediClubApp.Repositories.EFCore;

public class LogEFCoreRRepository : ILogRepository
{
    private readonly MyClinicDbContext _clinicDbContext;
    public LogEFCoreRRepository(MyClinicDbContext dbContext)
    {
        this._clinicDbContext = dbContext;
    }

    public async Task CreateAsync(Log newLog)
    {
       await this._clinicDbContext.Logs.AddAsync(entity: newLog);
       await this._clinicDbContext.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(int id)
    {
        var oldLog = await this._clinicDbContext.Logs.FirstOrDefaultAsync(d => d.Id == id);

        if(oldLog is null) return;

        this._clinicDbContext.Logs.Remove(entity: oldLog);
        await this._clinicDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Log>> GetAllAsync()
    {
        return this._clinicDbContext.Logs;
    }

    public Task<Log?> GetAsync(int id)
    {
        return this._clinicDbContext.Logs.FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task UpdateAsync(int id, Log newLog)
    {
        var oldLog = await this._clinicDbContext.Logs.FirstOrDefaultAsync(d => d.Id == id);

        if (oldLog is null) return;

        oldLog.Url = newLog.Url;
        oldLog.RequestBody = newLog.RequestBody;
        oldLog.ResponsetBody = newLog.ResponsetBody; 
        oldLog.CreationDate = newLog.CreationDate;
        oldLog.EndDate = newLog.EndDate;
        oldLog.StatusCode = newLog.StatusCode;
        oldLog.HttpMethod = newLog.HttpMethod;

        this._clinicDbContext.Update(oldLog);
        await this._clinicDbContext.SaveChangesAsync();
    }
}