using System.Data.SqlClient;
using Dapper;
using MediClubApp.Models;
using MediClubApp.Repositories.Base;

namespace MediClubApp.Repositories;
public class PatientDapperRepository : IPatientRepository
{
    private readonly string connectionString = "Server=localhost;Database=MediClubDb;TrustServerCertificate=True;Trusted_Connection=True;User Id=sa;Password=admin";

    public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
    {
        using var connection = new SqlConnection(connectionString);

        return await connection.QueryAsync<Patient>("SELECT * FROM Patient");
    }

    public async Task CreatePatientAsync(Patient newPatient)
    {
        if (newPatient is null)
        {
            throw new ArgumentNullException(nameof(newPatient));
        }

        using var connection = new SqlConnection(connectionString);

        var numberOfRows = await connection.ExecuteAsync(
            sql: $@"INSERT INTO Patient (FirstName, LastName, DateOfBirth, Gender, Email, MedicalHistory)
                        VALUES(@FirstName, @LastName, @DateOfBirth, @Gender, @Email, @MedicalHistory)",
            param:  newPatient
        );

        if (numberOfRows <= 0) throw new ArgumentException("Unsuccsess insert!");
    }
}
