using System.Text.Json;
using MediClubApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace MediClubApp.Controllers
{
    [Route("[controller]")]
    public class DoctorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [Route("[controller]")]
        public async Task<IActionResult> CreateDoctor(Doctor newDoctor)
        {
            if (string.IsNullOrWhiteSpace(newDoctor.FirstName) || string.IsNullOrWhiteSpace(newDoctor.LastName)) {return this.BadRequest();}

            
            var doctorsJson = await System.IO.File.ReadAllTextAsync("Assets/doctors.json");

            var doctors = JsonSerializer.Deserialize<List<Doctor>>(doctorsJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });

            
            doctors ??= new List<Doctor>();
            newDoctor.Id = doctors.Count() == 0 ? 1 : doctors.LastOrDefault()!.Id;
            doctors.Add(newDoctor);
            var newDoctorsJson = JsonSerializer.Serialize(doctors, new JsonSerializerOptions{
                PropertyNameCaseInsensitive = true,
            }); 

            await System.IO.File.WriteAllTextAsync("Assets/doctors.json", newDoctorsJson);
            
            return base.RedirectToAction(actionName: "Index");
        }
    }
}