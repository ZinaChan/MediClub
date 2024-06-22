using System.Text.Json;
using FluentValidation;
using MediClubApp.Models;
using MediClubApp.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace MediClubApp.Controllers;

[Route("[controller]")]
public class PatientController : Controller
{
    private readonly IPatientService _patientService;
    private readonly IValidator<Patient> _validator;

    public PatientController(IValidator<Patient> validator, IPatientService patientService)
    {
        this._validator = validator;
        this._patientService = patientService;
    }

    [HttpGet]
    [Route("/[controller]")]
    public async Task<IActionResult> Index()
    {
        var patients = await this._patientService.GetAllPatientsAsync();

        return View(patients);
    }

    [HttpGet]
    [Route("[action]", Name = "CreatePatientPage")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Route("[action]", Name = "CreatePatientApi")]
    public async Task<IActionResult> Create(Patient newPatient)
    {
        try
        {
            var validatorResult = this._validator.Validate(newPatient);
            if (!validatorResult.IsValid)
            {
                foreach (var error in validatorResult.Errors)
                {
                    base.ModelState.AddModelError(key: error.PropertyName, errorMessage: error.ErrorMessage);
                }

                return base.View("Create");
            }

            await this._patientService.CreatePatientAsync(newPatient);
            return base.RedirectToAction(actionName: "Index");
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
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

    [HttpPut]
    public async Task<IActionResult> UpdateDoctor([FromBody] Patient patient)
    {
        try
        {
            await this._patientService.UpdatePatientAsync(patient.Id, patient);
            return Ok();
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("{patientId:int}")]
    public async Task<IActionResult> DeleteDoctor(int patientId)
    {
        try
        {
            await this._patientService.DeletePatientByIdAsync(patientId);
            return Ok();
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
