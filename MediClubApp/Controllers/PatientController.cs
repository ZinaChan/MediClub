using System.Text.Json;
using MediClubApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace MediClubApp.Controllers
{
    [Route("[controller]")]
    public class PatientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("[controller]")]
        public async Task<IActionResult> CreatePatient(Patient newPatient)
        {
           if(string.IsNullOrWhiteSpace(newPatient.FirstName) & string.IsNullOrWhiteSpace(newPatient.LastName))
           {
            return this.BadRequest();
           } 

            var patientsJson = await System.IO.File.ReadAllTextAsync("Assets/patients.json");
            var patients = JsonSerializer.Deserialize<List<Patient>>(patientsJson, new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true, 
            });

            patients ??=  new List<Patient>();
            newPatient.Id = patients.Count() == 0 ? 1 : patients.LastOrDefault()!.Id;
            patients.Add(newPatient);

            var newPatientsJson = JsonSerializer.Serialize(patients, new JsonSerializerOptions{
                PropertyNameCaseInsensitive = true,
            });

            return base.RedirectToAction(actionName: "Index"); 
        }
    }
}