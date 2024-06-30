using System.Text.Json;
using FluentValidation;
using MediClubApp.Models;
using MediClubApp.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace MediClubApp.Controllers;

[Route("[controller]")]
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IValidator<User> _validator;

    public UserController(IValidator<User> validator, IUserService userService)
    {
        this._validator = validator;
        this._userService = userService;
    }

    [HttpGet]
    [Route("/[controller]")]
    public async Task<IActionResult> Index()
    {
        var users = await this._userService.GetAllUsersAsync();

        return base.View(null);
    }

    [HttpGet("Json")]
    public async Task<IActionResult> GetAllUsersJson()
    {
        try
        {
            var users = await this._userService.GetAllUsersAsync(); 
            return base.Json(data: users);
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [HttpGet]
    [Route("Json/{userId:Guid}")]
    public async Task<IActionResult> GetUserJson(Guid userId)
    {
        try
        {
            var user = await this._userService.GetUserAsync(id: userId);
            return base.Json(data: user);
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [HttpGet]
    [Route("/[controller]/{userId:Guid}")]
    public async Task<IActionResult> UserInfo(Guid userId)
    {
        try
        {
            var doctor = await this._userService.GetUserAsync(id: userId);
            return base.View(doctor);
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [HttpGet]
    [Route("[action]", Name = "CreateUserPage")]
    public IActionResult Create()
    {
        return base.View();
    }

    [HttpPost]
    [Route("[action]", Name = "CreateUserApi")]
    public async Task<IActionResult> Create(User newUser)
    {
        try
        {
            var validatorResult = this._validator.Validate(instance: newUser);
            if (!validatorResult.IsValid)
            {
                foreach (var error in validatorResult.Errors)
                {
                    base.ModelState.AddModelError(key: error.PropertyName, errorMessage: error.ErrorMessage);
                }

                return base.View(viewName: "Create");
            }

            await this._userService.CreateUserAsync(newUser: newUser);
            return base.RedirectToAction(actionName: "Index");
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateDoctor([FromBody] User user)
    {
        try
        {
            await this._userService.UpdateUserAsync(id: user.Id, newUser: user);
            return base.Ok();
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [HttpDelete("{userId:Guid}")]
    public async Task<IActionResult> DeleteDoctor(Guid userId)
    {
        try
        {
            await this._userService.DeleteUserByIdAsync(id: userId);
            return Ok();
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }
}