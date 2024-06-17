using System.Text.Json;
using MediClubApp.Models;
using MediClubApp.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace MediClubApp.Controllers;

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

    [HttpGet("{patientId}")]
    public async Task<IActionResult> PatientInfo(int patientId)
    {
        try
        {
            var patient = await _patientService.GetPatientAsync(patientId);
            return View(patient);
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreatePatient(Patient newPatient)
    {
        try
        {
            await this._patientService.CreatePatientAsync(newPatient);
            return base.RedirectToAction(actionName: "Index");
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    // [HttpPut]
    // public IActionResult UpdatePatient([FromBody] int id, Patient patient)
    // {
    //     try
    //     {
    //         this._patientService.UpdatePatientAsync(id, patient);
    //         return Ok();
    //     }
    //     catch (System.Exception ex)
    //     {
    //         return base.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    //     }
    // }
    
    // [HttpDelete]
    // public IActionResult DeletePatient([FromBody] Patient patient)
    // {
    //     try
    //     {
    //         this._patientService.DeletePatientAsync(patient);
    //         return Ok();
    //     }
    //     catch (System.Exception ex)
    //     {
    //         return base.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    //     }
    // }
    
}
