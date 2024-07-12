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

        return base.View(patients);
    }

    [HttpGet("Json")]
    public async Task<IActionResult> GetAllPatientsJson()
    {
        try
        {
            var patients = await this._patientService.GetAllPatientsAsync();
            foreach (var patient in patients)
            {
                patient.Appointments = null!;
                patient.MedicalRecords = null!;
            }
            return base.Json(data: patients);
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [HttpGet]
    [Route("Json/{patientId:Guid}")]
    public async Task<IActionResult> GetPatientJson(Guid patientId)
    {
        try
        {
            var patient = await this._patientService.GetPatientAsync(id: patientId);
            return base.Json(data: patient);
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [HttpGet]
    [Route("/[controller]/{patientId:Guid}")]
    public async Task<IActionResult> PatientInfo(Guid patientId)
    {
        try
        {
            var doctor = await this._patientService.GetPatientAsync(id: patientId);
            return base.View(doctor);
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [HttpGet]
    [Route("[action]", Name = "CreatePatientPage")]
    public IActionResult Create()
    {
        return base.View();
    }

    [HttpPost]
    [Route("[action]", Name = "CreatePatientApi")]
    public async Task<IActionResult> Create(Patient newPatient)
    {
        try
        {
            var validatorResult = this._validator.Validate(instance: newPatient);
            if (!validatorResult.IsValid)
            {
                foreach (var error in validatorResult.Errors)
                {
                    base.ModelState.AddModelError(key: error.PropertyName, errorMessage: error.ErrorMessage);
                }

                return base.View(viewName: "Create");
            }

            await this._patientService.CreatePatientAsync(newPatient: newPatient);
            return base.RedirectToAction(actionName: "Index");
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateDoctor([FromBody] Patient patient)
    {
        try
        {
            await this._patientService.UpdatePatientAsync(id: patient.Id, newPatient: patient);
            return base.Ok();
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [HttpDelete("{patientId:Guid}")]
    public async Task<IActionResult> DeleteDoctor(Guid patientId)
    {
        try
        {
            await this._patientService.DeletePatientByIdAsync(id: patientId);
            return Ok();
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }
}