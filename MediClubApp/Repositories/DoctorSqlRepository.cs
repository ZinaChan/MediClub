using System.Data.SqlClient;
using MediClubApp.Models;
using Dapper;

namespace MediClubApp.Repositories;
public class DoctorSqlRepository
{
    private readonly string _connectionString;
    public DoctorSqlRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            return await connection.QueryAsync<Doctor>("SELECT * FROM Doctors");
        }
    }
     
    public async void AddDoctorAsync(Doctor doctor)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            await connection.ExecuteAsync("INSERT INTO Doctors (Name, Surname, BirthDate, Phone, Email, Specialization) VALUES (@Name, @Surname, @BirthDate, @Phone, @Email, @Specialization)", doctor);
        }
    }
    public async void UpdateDoctorAsync(Doctor doctor)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            await connection.ExecuteAsync("UPDATE Doctors SET Name = @Name, Surname = @Surname, BirthDate = @BirthDate, Phone = @Phone, Email = @Email, Specialization = @Specialization  WHERE Id = @Id", doctor);
        }
    }
    public async void DeleteDoctorAsync(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            await connection.ExecuteAsync("DELETE FROM Doctors WHERE Id = @Id", new { Id = id });
        }
    }
    
}
