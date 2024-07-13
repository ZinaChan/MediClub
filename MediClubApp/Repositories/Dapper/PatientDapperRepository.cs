using System.Data.SqlClient;
using Dapper;
using MediClubApp.Models;
using MediClubApp.Options;
using MediClubApp.Options.Base;
using MediClubApp.Repositories.Base;
using Microsoft.Extensions.Options;

namespace MediClubApp.Repositories.Dapper;
public class PatientDapperRepository : IPatientRepository
{
    private readonly IConnectionStringOption _connectionStringOption;
    public PatientDapperRepository(IOptionsSnapshot<MsSqlConnectionOption> options)
    {
        this._connectionStringOption = options.Value;
    }

    public async Task<Patient?> GetAsync(Guid id)
    {
        using var connection = new SqlConnection(this._connectionStringOption.ConnectionString);

        return await connection.QueryFirstOrDefaultAsync<Patient>(sql: "SELECT * FROM Patients WHERE Id = @Id", param: new { Id = id });
    }
    public async Task<IEnumerable<Patient>> GetAllAsync()
    {
        using var connection = new SqlConnection(this._connectionStringOption.ConnectionString);

        return await connection.QueryAsync<Patient>(sql: "SELECT * FROM Patients");
    }
    public async Task CreateAsync(Patient newPatient)
    {
        if (newPatient is null)
        {
            throw new ArgumentNullException(nameof(newPatient));
        }

        using var connection = new SqlConnection(this._connectionStringOption.ConnectionString);
        newPatient.Id = new Guid();
        var numberOfRows = await connection.ExecuteAsync(
            sql: $@"INSERT INTO Patients (FirstName, LastName, DateOfBirth, Gender, Email, Address, PhoneNumber, MedicalHistory)
                        VALUES(@FirstName, @LastName, @DateOfBirth, @Gender, @Email, @Address, @PhoneNumber, @MedicalHistory)",
            param: newPatient
        );

        if (numberOfRows <= 0) throw new ArgumentException("Unsuccsess insert!");
    }
    public async Task UpdateAsync(Guid id, Patient newPatient)
    {
        using var connection = new SqlConnection(this._connectionStringOption.ConnectionString);

        await connection.ExecuteAsync(
            sql: $@"UPDATE Patients SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, Gender = @Gender, Email = @Email, Address = @Address, PhoneNumber = @PhoneNumber, MedicalHistory = @MedicalHistory
                        WHERE Id = @Id",
            param: newPatient
        );
    }
    public async Task DeleteByIdAsync(Guid id)
    {
        using var connection = new SqlConnection(this._connectionStringOption.ConnectionString);

        await connection.ExecuteAsync(
            sql: "DELETE FROM Patients WHERE Id = @Id",
            param: new { Id = id });
    }

    public Task<IEnumerable<Patient>> GetPatientsByDoctorAsync(Guid doctorId)
    {
        throw new NotImplementedException();
    }
}
