using FluentValidation;
using MediClubApp.Models;
using MediClubApp.Services.Base;
using MediClubApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediClubApp.Controllers;

[Authorize]
[Route("[controller]")]
public class MedicalRecordController : Controller
{
    private readonly IValidator<MedicalRecord> _validator;
    private readonly IMedicalRecordService _medicalRecordService;
    private readonly IPatientService _patientService;
    private readonly IDoctorService _doctorService;

    public MedicalRecordController(IValidator<MedicalRecord> validator, IMedicalRecordService medicalRecordService, IDoctorService doctorService, IPatientService patientService)
    {
        this._validator = validator;
        this._medicalRecordService = medicalRecordService;
        this._doctorService = doctorService;
        this._patientService = patientService;
    }

    [HttpGet]
    [Route("/[controller]/{id:Guid?}", Name ="MRIndex")]
    public async Task<IActionResult> Index(Guid? id = null)
    {
        var model = new MedicalRecordViewModel();
        if (User.IsInRole("Admin"))
        {
            model = new MedicalRecordViewModel
            {
                Patients = await _patientService.GetAllPatientsAsync(),
                Doctors = await _doctorService.GetAllDoctorsAsync(),
                MedicalRecords = await _medicalRecordService.GetAllMedicalRecordsAsync()
            };
        }
        else if (User.IsInRole("Doctor"))
        {
            if (id != null)
            {
                model = new MedicalRecordViewModel
                {
                    MedicalRecords = await _medicalRecordService.GetMedicalRecordsForDoctorAsync(doctorId: id.Value),
                };
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }
        else if (User.IsInRole("Patient"))
        {
            if (id != null)
            {
                model = new MedicalRecordViewModel
                {
                    MedicalRecords = await _medicalRecordService.GetMedicalRecordsForPatientAsync(patientId: id.Value)

                };
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        return View(model);
    }

    [HttpGet]
    [Route("Json")]
    public async Task<IActionResult> GetAllMedicalsRecord(Guid medicalRecordId)
    {
        try
        {
            var medicalRecords = await this._medicalRecordService.GetAllMedicalRecordsAsync();
            foreach (var medicalRecord in medicalRecords)
            {
                medicalRecord.Patient = null!;
                medicalRecord.Doctor = null!;
            }
            return base.Json(data: medicalRecords);
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [HttpGet]
    [Route("Json/{medicalRecordId:Guid}")]
    public async Task<IActionResult> GetMedicalRecord(Guid medicalRecordId)
    {
        try
        {
            var medicalRecord = await this._medicalRecordService.GetMedicalRecordAsync(id: medicalRecordId);
            return base.Json(data: medicalRecord);
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [HttpGet]
    [Route("/[controller]/[action]/{medicalRecordId:Guid}")]
    public async Task<IActionResult> MedicalRecordInfo(Guid medicalRecordId)
    {
        try
        {
            var medicalRecord = await this._medicalRecordService.GetMedicalRecordAsync(id: medicalRecordId);
            if (medicalRecord == null)
            {
                return NotFound();
            }
            medicalRecord!.Doctor = await this._doctorService.GetDoctorAsync(id: medicalRecord.DoctorId) ?? new Doctor();
            medicalRecord!.Patient = await this._patientService.GetPatientAsync(id: medicalRecord.PatientId) ?? new Patient();

            return base.View(model: medicalRecord);
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }


    [Authorize(Roles = "Admin")]
    [HttpGet]
    [Route("[action]", Name = "CreateMedicalRecordPage")]
    public async Task<IActionResult> Create()
    {
        var doctors = await this._doctorService.GetAllDoctorsAsync();
        var patients = await this._patientService.GetAllPatientsAsync();

        var model = new MedicalRecordViewModel
        {
            Doctors = doctors,
            Patients = patients,
            MedicalRecord = new MedicalRecord()
        };

        return base.View(model: model);
    }

    [Authorize(Policy = "MediClubPolicyUserRoles")] 
    [HttpPost(Name = "CreateMedicalRecordApi")]
    public async Task<IActionResult> Create(MedicalRecordViewModel model)
    {
        try
        {
            var validatorResult = this._validator.Validate(instance: model.MedicalRecord);
            if (!validatorResult.IsValid)
            {
                foreach (var error in validatorResult.Errors)
                {
                    base.ModelState.AddModelError(key: error.PropertyName, errorMessage: error.ErrorMessage);
                }

                model = new MedicalRecordViewModel
                {
                    Doctors = await this._doctorService.GetAllDoctorsAsync(),
                    Patients = await this._patientService.GetAllPatientsAsync(),
                    MedicalRecord = new MedicalRecord()
                };

                return base.View(viewName: "Create", model: model);
            }
            await this._medicalRecordService.CreateMedicalRecordAsync(newMedicalRecord: model.MedicalRecord);
            return base.RedirectToAction(actionName: "Index");
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }


    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<IActionResult> UpdateMedicalRecord([FromBody] MedicalRecord medicalRecord)
    {
        try
        {
            await this._medicalRecordService.UpdateMedicalRecordAsync(id: medicalRecord.Id, newMedicalRecord: medicalRecord);
            return base.Ok();
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{medicalRecordId:Guid}")]
    public async Task<IActionResult> DeleteMedicalRecord(Guid medicalRecordId)
    {
        try
        {
            await this._medicalRecordService.DeleteMedicalRecordByIdAsync(id: medicalRecordId);
            return base.Ok();
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }
}