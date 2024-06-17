using System.Text.Json;
using MediClubApp.Models;
using MediClubApp.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace MediClubApp.Controllers
{
    [Route("[controller]")]
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            this._patientService = patientService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var patients = await this._patientService.GetAllPatientsAsync();
            return View(patients);
        }

        [HttpPost] 
        public async Task<IActionResult> CreatePatient(Patient newPatient)
        {
           try{
                await this._patientService.CreatePatientAsync(newPatient);
                return base.RedirectToAction(actionName: "Index");
           }
           catch (System.Exception ex)
           {
                return base.BadRequest(ex.Message);
           }
        }
    }
}