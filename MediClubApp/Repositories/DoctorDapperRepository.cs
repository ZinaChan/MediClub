using System.Data.SqlClient;
using Dapper;
using MediClubApp.Models;
using MediClubApp.Repositories.Base;

namespace MediClubApp.Repositories;
public class DoctorDapperRepository : IDoctorRepository
{
    private readonly string connectionString = "Server=localhost;Database=MediClubDb;TrustServerCertificate=True;Trusted_Connection=True;User Id=sa;Password=admin";

    public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
    {
        using var connection = new SqlConnection(connectionString);

        return await connection.QueryAsync<Doctor>("SELECT * FROM Doctors");
    }

    public async Task CreateDoctorAsync(Doctor doctor)
    {
        if (doctor is null)
        {
            throw new ArgumentNullException(nameof(doctor));
        }

        using var connection = new SqlConnection(connectionString);

        var numberOfRows = await connection.ExecuteAsync(
            sql: $@"INSERT INTO Doctor (FirstName, LastName, DateOfBirth, Email)
                        VALUES(@FirstName, @LastName, @DateOfBirth, @Email)",
            param:  doctor
        );

        if (numberOfRows <= 0) throw new ArgumentException("Unsuccsess insert!");

    }
}
