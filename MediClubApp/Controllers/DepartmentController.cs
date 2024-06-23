using FluentValidation;
using MediClubApp.Models;
using MediClubApp.Services.Base;
using MediClubApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MediClubApp.Controllers;

[Route("[controller]")]
public class DepartmentController : Controller
{
    private readonly IValidator<Department> _validator;
    private readonly IDepartmentService _departmentService;
    public DepartmentController(IValidator<Department> validator, IDepartmentService departmentService)
    {
        this._validator = validator;
        this._departmentService = departmentService;
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
    [Route("Json/{departmentId:int}")]
    public async Task<IActionResult> GetDepartmentJson(int departmentId)
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
    [Route("/[controller]/{departmentId:int}")]
    public async Task<IActionResult> DepartmentInfo(int departmentId)
    {
        try
        {
            var department = await this._departmentService.GetDepartmentAsync(id: departmentId);
            return base.View(model: department);
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [HttpGet]
    [Route("[action]", Name = "CreateDepartmentPage")]
    public IActionResult Create()
    {
        return base.View();
    }

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

    [HttpDelete("{departmentId:int}")]
    public async Task<IActionResult> DeleteDepartment(int departmentId)
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