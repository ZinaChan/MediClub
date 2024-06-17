using System.Text.Json;
using MediClubApp.Models;
using MediClubApp.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace MediClubApp.Controllers
{
    [Route("[controller]")]
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            this._doctorService = doctorService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var doctors = await this._doctorService.GetAllDoctorsAsync();
            return View(doctors);
        }

        [HttpPost] 
        public async Task<IActionResult> CreateDoctor(Doctor newDoctor)
        {
           try{
                await this._doctorService.CreateDoctorAsync(newDoctor);
                return base.RedirectToAction(actionName: "Index");
           }
           catch (System.Exception ex)
           {
                return base.BadRequest(ex.Message);
           }
        }
    }
}