using FluentValidation;
using MediClubApp.Models;
using MediClubApp.Services.Base;
using MediClubApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediClubApp.Controllers;

[Authorize]
[Route("[controller]")]
public class DoctorController : Controller
{
    private readonly IDoctorService _doctorService;
    private readonly IDepartmentService _departmentService;
    private readonly ISpecializationService _specializationService;
    private readonly IValidator<Doctor> _validator;

    public DoctorController(IValidator<Doctor> validator, IDoctorService doctorService, IDepartmentService departmentService, ISpecializationService specializationService)
    {
        this._validator = validator;
        this._doctorService = doctorService;
        this._departmentService = departmentService;
        this._specializationService = specializationService;
    }

    [HttpGet]
    [Route("/[controller]")]
    public async Task<IActionResult> Index()
    {
        var model = new DoctorViewModel
        {
            Departments = await this._departmentService.GetAllDepartmentsAsync(),
            Specializations = await this._specializationService.GetAllSpecializationsAsync(),
            Doctors = await this._doctorService.GetAllDoctorsAsync()
        };
        return base.View(model);
    }

    [HttpGet]
    [Route("Json")]
    public async Task<IActionResult> GetAllDoctorsJson()
    {
        try
        {
            var doctors = await this._doctorService.GetAllDoctorsAsync();
            foreach (var doctor in doctors)
            {
                doctor.Specialization = null!;
                doctor.Department = null!;
            }
            return base.Json(data: doctors);
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [HttpGet]
    [Route("Json/{doctorId:Guid}")]
    public async Task<IActionResult> GetDoctorJson(Guid doctorId)
    {
        try
        {
            var doctor = await this._doctorService.GetDoctorAsync(id: doctorId);
            return base.Json(data: doctor);
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [HttpGet]
    [Route("/[controller]/{doctorId:Guid}")]
    public async Task<IActionResult> DoctorInfo(Guid doctorId)
    {
        try
        {
            var doctor = await this._doctorService.GetDoctorAsync(id: doctorId);
            doctor!.Department = await this._departmentService.GetDepartmentAsync(id: doctor.DepartmentId) ?? new Department();
            doctor!.Specialization = await this._specializationService.GetSpecializationAsync(id: doctor.SpecializationId) ?? new Specialization();

            return base.View(model: doctor);
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    [Route("[action]", Name = "CreateDoctorPage")]
    public async Task<IActionResult> Create()
    {
        var model = new DoctorViewModel
        {
            Departments = await this._departmentService.GetAllDepartmentsAsync(),
            Specializations = await this._specializationService.GetAllSpecializationsAsync(),
            Doctor = new Doctor()
        };

        return base.View(model: model);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost(Name = "CreateDoctorApi")]
    public async Task<IActionResult> Create(DoctorViewModel model)
    {
        try
        {
            var validatorResult = this._validator.Validate(instance: model.Doctor);
            if (!validatorResult.IsValid)
            {
                foreach (var error in validatorResult.Errors)
                {
                    base.ModelState.AddModelError(key: error.PropertyName, errorMessage: error.ErrorMessage);
                }

                model = new DoctorViewModel
                {
                    Departments = await this._departmentService.GetAllDepartmentsAsync(),
                    Specializations = await this._specializationService.GetAllSpecializationsAsync(),
                    Doctor = new Doctor()
                };
                return base.View(viewName: "Create", model: model);
            }

            await this._doctorService.CreateDoctorAsync(newDoctor: model.Doctor);
            return base.RedirectToAction(actionName: "Index");
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<IActionResult> UpdateDoctor([FromBody] Doctor doctor)
    {
        try
        {
            await this._doctorService.UpdateDoctorAsync(id: doctor.Id, newDoctor: doctor);
            return base.Ok();
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{doctorId:Guid}")]
    public async Task<IActionResult> DeleteDoctor(Guid doctorId)
    {
        try
        {
            await this._doctorService.DeleteDoctorByIdAsync(id: doctorId);
            return base.Ok();
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }
}