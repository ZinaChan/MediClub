using MediClubApp.Models;
using MediClubApp.Services.Base;
using MediClubApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediClubApp.Controllers;

[Authorize]
[Route("Admin/[controller]")]
public class DashBoardController : Controller
{
    private readonly IDepartmentService _departmentService;
    private readonly IUserService _userService; 

    public DashBoardController(IUserService userService, IDepartmentService departmentService)
    {
        this._userService = userService;
        this._departmentService = departmentService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var viewModel = new DashboardViewModel
        {
            Users = await this._userService.GetAllUsersAsync(), 
            TotalUsers = (await this._userService.GetAllUsersAsync()).Count(),  
        };
        return View(viewModel);
    }
}