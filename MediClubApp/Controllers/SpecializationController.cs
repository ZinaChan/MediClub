using FluentValidation;
using MediClubApp.Models;
using MediClubApp.Services.Base;
using MediClubApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MediClubApp.Controllers;

[Route("[controller]")]
public class SpecializationController : Controller
{
    private readonly IValidator<Specialization> _validator;
    private readonly ISpecializationService _specializationService;
    private readonly IDepartmentService _departmenttService;

    public SpecializationController(IValidator<Specialization> validator, ISpecializationService specializationService, IDepartmentService departmenttService)
    {
        this._validator = validator;
        this._specializationService = specializationService;
        this._departmenttService = departmenttService;
    }

    [HttpGet]
    [Route("/[controller]")]
    public async Task<IActionResult> Index()
    {
        var model = new SpecializationViewModel
        {
            Departments = await this._departmenttService.GetAllDepartmentsAsync(),
            Specializations = await this._specializationService.GetAllSpecializationsAsync()
        }; 
        return base.View(model); 
    }

    [HttpGet]
    [Route("Json")]
    public async Task<IActionResult> GetAllSpecializationsJson()
    {
        try
        {
            var specializations = await this._specializationService.GetAllSpecializationsAsync();
            foreach (var specialization in specializations)
            {
                specialization.Department = null!;
            }
            return base.Json(data: specializations);
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [HttpGet]
    [Route("Json/{specializationId:int}")]
    public async Task<IActionResult> GetSpecializationJson(int specializationId)
    {
        try
        {
            var specialization = await this._specializationService.GetSpecializationAsync(id: specializationId);
            return base.Json(data: specialization);
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [HttpGet]
    [Route("/[controller]/{specializationId:int}")]
    public async Task<IActionResult> SpecializationInfo(int specializationId)
    {
        try
        {
            var specialization = await this._specializationService.GetSpecializationAsync(id: specializationId);
            specialization!.Department = await this._departmenttService.GetDepartmentAsync(id: specialization.DepartmentId) ?? new Department();

            return base.View(model: specialization);
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [HttpGet]
    [Route("[action]", Name = "CreateSpecializationPage")]
    public async Task<IActionResult> Create()
    {
        var departments = await this._departmenttService.GetAllDepartmentsAsync();

        var model = new SpecializationViewModel
        {
            Departments = departments,
            Specialization = new Specialization()
        };

        return base.View(model);
    }

    [HttpPost(Name = "CreateSpecializationApi")]
    public async Task<IActionResult> Create(SpecializationViewModel model)
    {
        try
        {
            var validatorResult = this._validator.Validate(instance: model.Specialization);
            if (!validatorResult.IsValid)
            {
                foreach (var error in validatorResult.Errors)
                {
                    base.ModelState.AddModelError(key: error.PropertyName, errorMessage: error.ErrorMessage);
                }

                var departments = await this._departmenttService.GetAllDepartmentsAsync();
                model = new SpecializationViewModel
                {
                    Departments = departments,
                    Specialization = new Specialization()
                };
                return base.View(viewName: "Create", model: model);
            }
            await this._specializationService.CreateSpecializationAsync(newSpecialization: model.Specialization);
            return base.RedirectToAction(actionName: "Index");
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSpecialization([FromBody] Specialization specialization)
    {
        try
        {
            await this._specializationService.UpdateSpecializationAsync(id: specialization.Id, newSpecialization: specialization);
            return base.Ok();
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [HttpDelete("{specializationId:int}")]
    public async Task<IActionResult> DeleteSpecialization(int specializationId)
    {
        try
        {
            await this._specializationService.DeleteSpecializationByIdAsync(id: specializationId);
            return base.Ok();
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }
}