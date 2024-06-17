using System.Data.SqlClient;
using Dapper;
using MediClubApp.Models;
using MediClubApp.Repositories.Base;

namespace MediClubApp.Repositories;
public class DoctorDapperRepository : IDoctorRepository
{
    private readonly string connectionString = "Server=localhost;Database=MediClubDb;TrustServerCertificate=True;Trusted_Connection=True;User Id=sa;Password=admin";

    public async Task<Doctor?> GetAsync(int id)
    {
        using var connection = new SqlConnection(connectionString);

        return await connection.QueryFirstOrDefaultAsync<Doctor>(sql: "SELECT * FROM Doctor WHERE Id = @Id", param: new { Id = id });
    }
    public async Task<IEnumerable<Doctor>> GetAllAsync()
    {
        using var connection = new SqlConnection(connectionString);

        return await connection.QueryAsync<Doctor>("SELECT * FROM Doctors");
    }
    public async Task CreateAsync(Doctor newDoctor)
    {
        if (newDoctor is null)
        {
            throw new ArgumentNullException(nameof(newDoctor));
        }

        using var connection = new SqlConnection(connectionString);

        var numberOfRows = await connection.ExecuteAsync(
            sql: $@"INSERT INTO Doctors (FirstName, LastName, DateOfBirth, Gender, Email, PhoneNumber, Specialization, Department )
                        VALUES(@FirstName, @LastName, @DateOfBirth, @Gender, @Email, @PhoneNumber, @Specialization, @Department)",
            param: newDoctor
        );

        if (numberOfRows <= 0) throw new ArgumentException("Unsuccsess insert!");

    }
    public async Task UpdateAsync(int id, Doctor newDoctor)
    {
        using var connection = new SqlConnection(connectionString);

        await connection.ExecuteAsync(
            sql: "UPDATE Doctors SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, Gender = @Gender, Email = @Email, PhoneNumber = @PhoneNumber , Specialization = @Specialization, Department = @Department WHERE Id = @Id",
            param: newDoctor);
    }
    public async Task DeleteAsync(Doctor oldDoctor)
    {
        using var connection = new SqlConnection(connectionString);

        await connection.ExecuteAsync(
            sql: "DELETE FROM Doctors WHERE Id = @Id",
            param: new { Id = oldDoctor.Id });
    }
}
