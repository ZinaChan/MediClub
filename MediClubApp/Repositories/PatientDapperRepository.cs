using System.Data.SqlClient;
using Dapper;
using MediClubApp.Models;
using MediClubApp.Repositories.Base;

namespace MediClubApp.Repositories;
public class PatientDapperRepository : IPatientRepository
{
    private readonly string connectionString = "Server=localhost;Database=MediClubDb;TrustServerCertificate=True;Trusted_Connection=True;User Id=sa;Password=admin";

    public async Task<Patient?> GetAsync(int id)
    {
        using var connection = new SqlConnection(connectionString);

        return await connection.QueryFirstOrDefaultAsync<Patient>( sql: "SELECT * FROM Patients WHERE Id = @Id", param:  new { Id = id }) ;   
    }
    public async Task<IEnumerable<Patient>> GetAllAsync()
    {
        using var connection = new SqlConnection(connectionString);

        return await connection.QueryAsync<Patient>( sql: "SELECT * FROM Patients");
    }
    public async Task CreateAsync(Patient newPatient)
    {
        if (newPatient is null)
        {
            throw new ArgumentNullException(nameof(newPatient));
        }

        using var connection = new SqlConnection(connectionString);

        var numberOfRows = await connection.ExecuteAsync(
            sql: $@"INSERT INTO Patients (FirstName, LastName, DateOfBirth, Gender, Email, Address, PhoneNumber, MedicalHistory)
                        VALUES(@FirstName, @LastName, @DateOfBirth, @Gender, @Email, @Address, @PhoneNumber, @MedicalHistory)",
            param: newPatient
        );

        if (numberOfRows <= 0) throw new ArgumentException("Unsuccsess insert!");
    }
    public async Task UpdateAsync(int id, Patient newPatient)
    {
        using var connection = new SqlConnection(connectionString);

        await connection.ExecuteAsync( 
            sql: "UPDATE Patients SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, Gender = @Gender, Email = @Email, Address = @Address, PhoneNumber = @PhoneNumber, MedicalHistory = @MedicalHistory WHERE Id = @Id", 
            param: newPatient
        );
    }
    public async Task DeleteByIdAsync(int id)
    {
        using var connection = new SqlConnection(connectionString);

        await connection.ExecuteAsync(
            sql: "DELETE FROM Patients WHERE Id = @Id", 
            param: new { Id = id });
    }
    
}
