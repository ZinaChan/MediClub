using System.Text.Json;
using MediClubApp.Models;
using MediClubApp.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace MediClubApp.Controllers;

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


    [HttpGet("{doctorId:int}")]
    public async Task<IActionResult> DoctorInfo(int doctorId)
    {
        try
        {
            var doctor = await _doctorService.GetDoctorAsync(doctorId);
            return View(doctor);
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateDoctor(Doctor newDoctor)
    {
        try
        {
            await this._doctorService.CreateDoctorAsync(newDoctor);
            return base.RedirectToAction(actionName: "Index");
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    // [HttpPut]
    // public IActionResult UpdateDoctor([FromBody] int id, Doctor doctor)
    // {
    //     try
    //     {
    //         this._doctorService.UpdateDoctorAsync(id, doctor);
    //         return Ok();
    //     }
    //     catch (System.Exception ex)
    //     {
    //         return base.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    //     }
    // }
    // [HttpDelete]
    // public IActionResult DeleteDoctor([FromBody] Doctor doctor)
    // {
    //     try
    //     {
    //         this._doctorService.DeleteDoctorAsync(doctor);
    //         return Ok();
    //     }
    //     catch (System.Exception ex)
    //     {
    //         return base.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    //     }
    // }
}
