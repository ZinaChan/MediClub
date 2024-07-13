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
    private readonly IUserService _userService; 
    private readonly IDoctorService _doctorService; 
    private readonly IPatientService _patientService; 
    private readonly IDepartmentService _departmentService;
    private readonly ISpecializationService _specializationService; 
    private readonly IRoomService _roomService; 

    public DashBoardController(IUserService userService, IDoctorService doctorService, IPatientService patientService, IDepartmentService departmentService, ISpecializationService specializationService, IRoomService roomService)
    {
        this._userService = userService;
        this._doctorService = doctorService;
        this._patientService = patientService;
        this._departmentService = departmentService;
        this._specializationService = specializationService;
        this._roomService = roomService;
    }

    [HttpGet(Name = "Services")]
    public async Task<IActionResult> Index()
    {
        var viewModel = new DashboardViewModel
        {
            TotalUsers = (await this._userService.GetAllUsersAsync()).Count(),  
            TotalDoctors = (await this._doctorService.GetAllDoctorsAsync()).Count(),  
            TotalPatients = (await this._patientService.GetAllPatientsAsync()).Count(),  
            TotalDepartments = (await this._departmentService.GetAllDepartmentsAsync()).Count(),  
            TotalSpecializations = (await this._specializationService.GetAllSpecializationsAsync()).Count(),  
            TotalRooms = (await this._roomService.GetAllRoomsAsync()).Count()
        };
        return View(viewModel);
    }
}