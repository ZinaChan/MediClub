using System.Data.SqlClient;
using Dapper;
using MediClubApp.Models;
using MediClubApp.Options;
using MediClubApp.Options.Base;
using MediClubApp.Repositories.Base;
using Microsoft.Extensions.Options;

namespace MediClubApp.Repositories.Dapper;
public class LogDapperRepository : ILogRepository
{
    private readonly IConnectionStringOption _connectionStringOption;
    public LogDapperRepository(IOptionsSnapshot<MsSqlConnectionOption> options)
    {
        this._connectionStringOption = options.Value;
    }

    public async Task<Log?> GetAsync(Guid id)
    {
        using var connection = new SqlConnection(this._connectionStringOption.ConnectionString);

        return await connection.QueryFirstOrDefaultAsync<Log>(sql: "SELECT * FROM Logs WHERE Id = @Id", param: new { Id = id });
    }
    public async Task<IEnumerable<Log>> GetAllAsync()
    {
        using var connection = new SqlConnection(this._connectionStringOption.ConnectionString);

        return await connection.QueryAsync<Log>("SELECT * FROM Logs");
    }
    public async Task CreateAsync(Log newLog)
    {
        if (newLog is null)
        {
            throw new ArgumentNullException(nameof(newLog));
        }

        using var connection = new SqlConnection(this._connectionStringOption.ConnectionString);
        newLog.Id = new Guid();
        var numberOfRows = await connection.ExecuteAsync(
            sql: $@"INSERT INTO Logs (Url, RequestBody, ResponsetBody, CreationDate, EndDate, StatusCode, HttpMethod )
                        VALUES(@Url, @RequestBody, @ResponsetBody, @CreationDate, @EndDate, @StatusCode, @HttpMethod)",
            param: newLog
        );

        if (numberOfRows <= 0) throw new ArgumentException("Unsuccsess insert!");
    }
    public async Task UpdateAsync(Guid id, Log newLog)
    {
        using var connection = new SqlConnection(this._connectionStringOption.ConnectionString);

        await connection.ExecuteAsync(
            sql: $@"UPDATE Logs SET Url = @Url, RequestBody = @RequestBody, ResponsetBody = @ResponsetBody, CreationDate = @CreationDate, EndDate = @EndDate, StatusCode = @StatusCode , HttpMethod = @HttpMethod 
                        WHERE Id = @Id",
            param: newLog); 
    }
    public async Task DeleteByIdAsync(Guid id)
    {
        using var connection = new SqlConnection(this._connectionStringOption.ConnectionString);

        await connection.ExecuteAsync(
            sql: "DELETE FROM Logs WHERE Id = @Id",
            param: new { Id = id });
    }
}
