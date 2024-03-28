// using System.Data.SqlClient;
// using MediClubApp.Models;
// using Dapper;

// namespace MediClubApp.Services;

// public class HospitalService
// {
//     private readonly string _connectionString;
//     public HospitalService(string connectionString)
//     {
//         _connectionString = connectionString;
//     }

//     public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
//     {
//         using (var connection = new SqlConnection(_connectionString))
//         {
//             connection.Open();
//             return await connection.QueryAsync<Doctor>("SELECT * FROM Doctors");
//         }
//     }
//     public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
//     {
//         using (var connection = new SqlConnection(_connectionString))
//         {
//             connection.Open();
//             return await connection.QueryAsync<Patient>("SELECT * FROM Patients");
//         }
//     }

//     public async void AddDoctorAsync(Doctor doctor)
//     {
//         using (var connection = new SqlConnection(_connectionString))
//         {
//             connection.Open();
//             await connection.ExecuteAsync("INSERT INTO Doctors (Name, Surname, BirthDate, Phone, Email, Specialization) VALUES (@Name, @Surname, @BirthDate, @Phone, @Email, @Specialization)", doctor);
//         }
//     }
//     public async void UpdateDoctorAsync(Doctor doctor)
//     {
//         using (var connection = new SqlConnection(_connectionString))
//         {
//             connection.Open();
//             await connection.ExecuteAsync("UPDATE Doctors SET Name = @Name, Surname = @Surname, BirthDate = @BirthDate, Phone = @Phone, Email = @Email, Specialization = @Specialization  WHERE Id = @Id", doctor);
//         }
//     }
//     public async void DeleteDoctorAsync(int id)
//     {
//         using (var connection = new SqlConnection(_connectionString))
//         {
//             connection.Open();
//             await connection.ExecuteAsync("DELETE FROM Doctors WHERE Id = @Id", new { Id = id });
//         }
//     }
//     public async void AddPatientAsync(Patient patient)
//     {
//         using (var connection = new SqlConnection(_connectionString))
//         {
//             connection.Open();
//             await connection.ExecuteAsync("INSERT INTO Patients (Name, Surname, BirthDate, Phone, Email, Diagnosis) VALUES (@Name, @Surname, @BirthDate, @Phone, @Email, @Diagnosis)", patient);
//         }
//     }
//     public async void UpdatePatientAsync(Patient patient)
//     {
//         using (var connection = new SqlConnection(_connectionString))
//         {
//             connection.Open();
//             await connection.ExecuteAsync("UPDATE Patients SET Name = @Name, Surname = @Surname, BirthDate = @BirthDate, Phone = @Phone, Email = @Email, Diagnosis = @Diagnosis  WHERE Id = @Id", patient);
//         }
//     }
//     public async void DeletePatientAsync(int id)
//     {
//         using (var connection = new SqlConnection(_connectionString))
//         {
//             connection.Open();
//             await connection.ExecuteAsync("DELETE FROM Patients WHERE Id = @Id", new { Id = id });
//         }
//     }
// }
