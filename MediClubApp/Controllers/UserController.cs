using System.Text.Json;
using FluentValidation;
using MediClubApp.Models;
using MediClubApp.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediClubApp.Controllers;


[Authorize]
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

        return base.View(users);
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
            if (doctor == null)
            {
                return NotFound();
            }

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
    [Route("[action]", Name = "CreateUserEndpoint")]
    public async Task<IActionResult> Registration([FromForm] User user, IFormFile image)
    {
        try
        {
            var users = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Gender = user.Gender,
                DateOfBirth = user.DateOfBirth,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
            };

            var validationResult = this._validator.Validate(user);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    base.ModelState.AddModelError(key: error.PropertyName, errorMessage: error.ErrorMessage);
                }
                return View();
            }

            await this._userService.CreateUserAsync(newUser: user, image: image);
        }
        catch (Exception ex)
        {
            TempData["error"] = ex.Message;
            return base.RedirectToRoute("RegistrationView");
        }

        return base.RedirectToRoute("LoginView");
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

    [HttpPost]
    public async Task<IActionResult> ChangeAvatar(Guid id, IFormFile avatarFile)
    {
        try
        {
            if (avatarFile == null || avatarFile.Length == 0)
            {
                return BadRequest("No file uploaded or file is empty.");
            }

            var avatarUrl = await this._userService.ChangeAvatar(id: id, formFile: avatarFile);
            return Json(new { avatarUrl });
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

}