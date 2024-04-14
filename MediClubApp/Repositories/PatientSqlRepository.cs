using System.Data.SqlClient;
using MediClubApp.Models;
using Dapper;

namespace MediClubApp.Repositories;
public class PatientSqlRepository
{
    private readonly string _connectionString;
    public PatientSqlRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            return await connection.QueryAsync<Patient>("SELECT * FROM Patients");
        }
    }
  
    public async void AddPatientAsync(Patient patient)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            await connection.ExecuteAsync("INSERT INTO Patients (Name, Surname, BirthDate, Phone, Email, Diagnosis) VALUES (@Name, @Surname, @BirthDate, @Phone, @Email, @Diagnosis)", patient);
        }
    }
    public async void UpdatePatientAsync(Patient patient)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            await connection.ExecuteAsync("UPDATE Patients SET Name = @Name, Surname = @Surname, BirthDate = @BirthDate, Phone = @Phone, Email = @Email, Diagnosis = @Diagnosis  WHERE Id = @Id", patient);
        }
    }
    public async void DeletePatientAsync(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            await connection.ExecuteAsync("DELETE FROM Patients WHERE Id = @Id", new { Id = id });
        }
    }
}
