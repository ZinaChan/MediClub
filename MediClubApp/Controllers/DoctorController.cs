using FluentValidation;
using MediClubApp.Models;
using MediClubApp.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace MediClubApp.Controllers;

[Route("[controller]")]
public class DoctorController : Controller
{
    private readonly IDoctorService _doctorService;
    private readonly IValidator<Doctor> _validator;

    public DoctorController(IValidator<Doctor> validator, IDoctorService doctorService)
    {
        this._validator = validator;
        this._doctorService = doctorService;
    }

    [HttpGet]
    [Route("/[controller]")]
    public async Task<IActionResult> Index()
    {
        var doctors = await this._doctorService.GetAllDoctorsAsync();
        return View(doctors);
    }

    [HttpGet]
    [Route("[action]", Name = "CreateDoctorPage")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost(Name = "CreateDoctorApi")]
    public async Task<IActionResult> Create(Doctor newDoctor)
    {
        try
        {
            var validatorResult = this._validator.Validate(newDoctor);
            if (!validatorResult.IsValid)
            {
                foreach (var error in validatorResult.Errors)
                {
                    base.ModelState.AddModelError(key: error.PropertyName, errorMessage: error.ErrorMessage);
                }

                return base.View("Create");
            }

            await this._doctorService.CreateDoctorAsync(newDoctor);
            return base.RedirectToAction(actionName: "Index");
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
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

    [HttpGet("Json/{doctorId:int}")]
    public async Task<IActionResult> GetDoctorJson(int doctorId)
    {
        try
        {
            var doctor = await _doctorService.GetDoctorAsync(doctorId);
            return Json(doctor);
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateDoctor([FromBody] Doctor doctor)
    {
        try
        {
            await this._doctorService.UpdateDoctorAsync(doctor.Id, doctor);
            return Ok();
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("{doctorId:int}")]
    public async Task<IActionResult> DeleteDoctor(int doctorId)
    {
        try
        {
            await this._doctorService.DeleteDoctorByIdAsync(doctorId);
            return Ok();
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
