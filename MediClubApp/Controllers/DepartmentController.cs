using FluentValidation;
using MediClubApp.Models;
using MediClubApp.Repositories.Base;
using MediClubApp.Services.Base;
using MediClubApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediClubApp.Controllers;

[Authorize]
[Route("[controller]")]
public class DepartmentController : Controller
{
    private readonly IValidator<Department> _validator;
    private readonly IDepartmentService _departmentService;
    private readonly IDoctorService _doctorService;
    public DepartmentController(IValidator<Department> validator, IDepartmentService departmentService, IDoctorRepository doctorRepository, IDoctorService doctorService)
    {
        this._validator = validator;
        this._departmentService = departmentService;
        this._doctorService = doctorService;
    }

    [HttpGet]
    [Route("/[controller]")]
    public async Task<IActionResult> Index()
    {
        var departments = await this._departmentService.GetAllDepartmentsAsync();
        return base.View(model: departments);
    }

    [HttpGet]
    [Route("Json")]
    public async Task<IActionResult> GetAllDepartmentsJson()
    {
        try
        {
            var departments = await this._departmentService.GetAllDepartmentsAsync();
            foreach (var department in departments)
            {
                department.Doctors = null!;
                department.Rooms = null!;
                department.Specializations = null!;
            }
            return base.Json(data: departments);
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [HttpGet]
    [Route("Json/{departmentId:Guid}")]
    public async Task<IActionResult> GetDepartmentJson(Guid departmentId)
    {
        try
        {
            var department = await this._departmentService.GetDepartmentAsync(id: departmentId);
            return base.Json(data: department);
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [HttpGet]
    [Route("/[controller]/{departmentId:Guid}")]
    public async Task<IActionResult> DepartmentInfo(Guid departmentId)
    {
        try
        {
            var department = await this._departmentService.GetDepartmentAsync(id: departmentId);
            if (department == null)
            {
                return NotFound();
            }
            department!.Doctors = await this._doctorService.GetDoctorsByDepartmentAsync(departmentId: departmentId);
            return base.View(model: department);
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    [Route("[action]", Name = "CreateDepartmentPage")]
    public IActionResult Create()
    {
        return base.View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost(Name = "CreateDepartmentApi")]
    public async Task<IActionResult> Create(Department department)
    {
        try
        {
            var validatorResult = this._validator.Validate(instance: department);
            if (!validatorResult.IsValid)
            {
                foreach (var error in validatorResult.Errors)
                {
                    base.ModelState.AddModelError(key: error.PropertyName, errorMessage: error.ErrorMessage);
                }

                return base.View(viewName: "Create");
            }
            await this._departmentService.CreateDepartmentAsync(newDepartment: department);
            return base.RedirectToAction(actionName: "Index");
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<IActionResult> UpdateDepartment([FromBody] Department department)
    {
        try
        {
            await this._departmentService.UpdateDepartmentAsync(id: department.Id, newDepartment: department);
            return base.Ok();
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{departmentId:Guid}")]
    public async Task<IActionResult> DeleteDepartment(Guid departmentId)
    {
        try
        {
            await this._departmentService.DeleteDepartmentByIdAsync(id: departmentId);
            return base.Ok();
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }
}
