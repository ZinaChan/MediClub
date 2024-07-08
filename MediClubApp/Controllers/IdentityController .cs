using System.Security.Claims;
using FluentValidation;
using MediClubApp.Dtos;
using MediClubApp.Models;
using MediClubApp.Services.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace MediClubApp.Controllers;

[Route("[controller]")]
public class IdentityController : Controller
{
    public IUserService _userService;
    private readonly IDataProtector _dataProtector;
    private readonly IValidator<User> _userValidator;
    public IdentityController(IValidator<User> userValidator, IUserService userService, IDataProtectionProvider dataProtectionProvider)
    {
        this._userService = userService;
        this._dataProtector = dataProtectionProvider.CreateProtector("identity");
        this._userValidator = userValidator;
    }

    [HttpGet]
    public IActionResult Log()
    {
        return base.View();
    }

    [HttpGet]
    [Route("[action]", Name = "LoginView")]
    public IActionResult Login(string? ReturnUrl)
    {
        var errorMessage = base.TempData["error"];

        ViewBag.ReturnUrl = ReturnUrl;

        if (errorMessage != null)
        {
            base.ModelState.AddModelError("All", errorMessage.ToString()!);
        }

        return base.View();
    }

    [HttpPost]
    [Route("[action]", Name = "LoginEndPoint")]
    public async Task<IActionResult> Login([FromForm] LoginDto loginDto)
    {
        try
        {
            var foundUser = await this._userService.GetUserAsync(loginDto.Email);

            if (foundUser == null || foundUser.Email != loginDto.Email || foundUser.Password != loginDto.Password)
            {
                base.TempData["error"] = "Incorrect login or password!";
                return base.RedirectToRoute("LoginView", new
                {
                    loginDto.ReturnUrl
                });
            }

            var claims = new List<Claim>
            {
                new Claim("Id", foundUser.Id.ToString()),
                new Claim(ClaimTypes.Email, foundUser.Email),
                new Claim(ClaimTypes.Name, foundUser.FirstName),
                new Claim(ClaimTypes.Surname, foundUser.LastName),
                new Claim("Image", foundUser.AvatarUrl!),
                new Claim(ClaimTypes.Role, foundUser.Role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await base.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            if (!string.IsNullOrWhiteSpace(loginDto.ReturnUrl))
            {
                return base.Redirect(loginDto.ReturnUrl);
            }

            return base.RedirectToAction(controllerName: "Patient", actionName: "Index");
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }


    [HttpGet]
    [Route("[action]", Name = "RegistrationView")]
    public IActionResult Registration()
    {
        if (TempData["error"] != null)
        {
            ModelState.AddModelError("All", "This Email already registered");
        }

        return base.View();
    }


    [HttpPost]
    [Route("[action]", Name = "RegistrationEndpoint")]
    public async Task<IActionResult> Registration([FromForm] RegistrationDto registrationDto, IFormFile image)
    {
        try
        {
            var user = new User
            {
                FirstName = registrationDto.FirstName,
                LastName = registrationDto.LastName,
                Email = registrationDto.Email,
                Password = registrationDto.Password,
                Gender = registrationDto.Gender,
                DateOfBirth = registrationDto.DateOfBirth,
                PhoneNumber = registrationDto.PhoneNumber,
                Address = registrationDto.Address,
            };

            var validationResult = this._userValidator.Validate(user);
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

    [Authorize]
    [HttpGet]
    [Route("[action]", Name = "LogOut")]
    public async Task<IActionResult> Logout(string? ReturnUrl)
    {

        await base.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return base.RedirectToRoute("LoginView", new
        {
            ReturnUrl
        });
    }

    [Authorize]
    [HttpGet]
    [Route("[action]", Name = "UserInfo")]
    public async Task<IActionResult> UserInfo()
    {

        try
        {
            var userIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == "Id");
            if (userIdClaim == null)
            {
                return Unauthorized("User ID claim not found");
            }

            if (!Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return BadRequest("Invalid user ID");
            }

            var user = await _userService.GetUserAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            return View(user);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
