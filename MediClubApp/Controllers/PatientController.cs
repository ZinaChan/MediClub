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

    [HttpGet("{patientId:int}")]
    public async Task<IActionResult> PatientInfo(int patientId)
    {
        try
        {
            var doctor = await _patientService.GetPatientAsync(patientId);
            return View(doctor);
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("Json/{patientId:int}")]
    public async Task<IActionResult> GetPatientJson(int patientId)
    {
        try
        {
            var doctor = await _patientService.GetPatientAsync(patientId);
            return Json(doctor);
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

    [HttpPut]
    public IActionResult UpdateDoctor([FromBody] Patient patient)
    {
        try
        {
            this._patientService.UpdatePatientAsync(patient.Id, patient);
            return Ok();
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("{patientId:int}")]
    public IActionResult DeleteDoctor(int patientId)
    {
        try
        {
            this._patientService.DeletePatientByIdAsync(patientId);
            return Ok();
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

}
