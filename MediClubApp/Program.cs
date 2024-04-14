using System.Net;
using MediClubApp.Controllers;
using MediClubApp.Controllers.Base;
using MediClubApp.Models;
using MediClubApp.Repositories;

var httpListener = new HttpListener();

var prefixes = "http://*:8080/";

httpListener.Prefixes.Add(prefixes);

httpListener.Start();

System.Console.WriteLine("Server started....");
System.Console.WriteLine("http://localhost:8080/");

const string connectionString = "Server=localhost;Database=MediClubDb;User Id=sa;Password=admin;";
var patientSqlRepository = new PatientSqlRepository(connectionString);
var doctorSqlRepository = new DoctorSqlRepository(connectionString);

// var newDoctor = new Doctor { Name = "John", Surname= "Smith", BirthDate = DateTime.Today, Phone = "+9940514556860", Email = "jsmith@mail", Specialization = "Cardiologist" };
// var newDoctor2 = new Doctor { Name = "Emma", Surname= "Wathson", BirthDate = DateTime.Today, Phone = "+9946324556860", Email = "emmyWat@mail", Specialization = "Dantist" };

// doctorSqlRepository.AddDoctorAsync(newDoctor2);
// doctorSqlRepository.DeleteDoctorAsync(5);

// var newPatient = new Patient { Name = "Olga", Surname = "Petrova", BirthDate = DateTime.Today, Phone = "+7080514556860", Email = "olgapeth@mail", Diagnosis = "high blood pressure" };
// var newPatient2 = new Patient { Name = "Ali", Surname = "Aliyev", BirthDate = DateTime.Today, Phone = "+9940516436860", Email = "alAli@mail", Diagnosis = "high blood pressure" };
// patientSqlRepository.AddPatientAsync(newPatient2);  
// patientSqlRepository.DeletePatientAsync(5);

while (true)
{
    var client = await httpListener.GetContextAsync();

    string? endpoint = client.Request.RawUrl;

    switch (endpoint)
    {
        case "/":
            var hc = new HomeController();
            await hc.Home(client);
            break;
        case "/Doctors":
            var dc = new DoctorsController(doctorSqlRepository);
            await dc.Doctors(client);
            break;
        case "/Patients":
            var pc = new PatientController(patientSqlRepository);
            await pc.Patients(client);
            break;
        default:
            var ec = new ErrorController();
            ec.NotFound(client, endpoint!);
            break;
    }

    using var streamWriter = new StreamWriter(client.Response.OutputStream);

    await streamWriter.WriteAsync(endpoint);

    await streamWriter.FlushAsync();
}