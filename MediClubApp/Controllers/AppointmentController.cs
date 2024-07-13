using FluentValidation;
using MediClubApp.Models;
using MediClubApp.Services.Base;
using MediClubApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediClubApp.Controllers;

[Authorize]
[Route("[controller]")]
public class AppointmentController : Controller
{
    private readonly IValidator<Appointment> _validator;
    private readonly IAppointmentService _appointmentService;
    private readonly IPatientService _patientService;
    private readonly IRoomService _roomService;
    private readonly IDoctorService _doctorService;

    public AppointmentController(IValidator<Appointment> validator, IAppointmentService appointmentService, IDoctorService doctorService, IPatientService patientService, IRoomService roomService)
    {
        this._validator = validator;
        this._appointmentService = appointmentService;
        this._patientService = patientService;
        this._roomService = roomService;
        this._doctorService = doctorService;
    }

    [HttpGet]
    [Route("/[controller]/{id:Guid?}", Name = "AppointmentIndex")]
    public async Task<IActionResult> Index(Guid? id = null)
    {
        var model = new AppointmentViewModel();
        if (User.IsInRole("Admin"))
        {
            model = new AppointmentViewModel
            {
                Patients = await _patientService.GetAllPatientsAsync(),
                Doctors = await _doctorService.GetAllDoctorsAsync(),
                Appointments = await _appointmentService.GetAllAppointmentsAsync(),
                Rooms = await this._roomService.GetAllRoomsAsync()
            };
        }
        else if (User.IsInRole("Doctor"))
        {
            if (id.Value != null)
            {
                model = new AppointmentViewModel
                {
                    Appointments = await _appointmentService.GetAppointmentsForDoctorAsync(doctorId: id.Value)
                };
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }
        else if (User.IsInRole("Patient"))
        {
            if (id.Value != null)
            {
                model = new AppointmentViewModel
                {
                    Appointments = await _appointmentService.GetAppointmentsForPatientAsync(patientId: id.Value)
                };
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        return View(model);
    }

    [HttpGet]
    [Route("Json")]
    public async Task<IActionResult> GetAllAppointmentsJson()
    {
        try
        {
            var appointments = await this._appointmentService.GetAllAppointmentsAsync();
            foreach (var appointment in appointments)
            {
                appointment.Patient = null!;
                appointment.Doctor = null!;
                appointment.Room = null!;
            }
            return base.Json(data: appointments);
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [HttpGet]
    [Route("Json/{appointmentId:Guid}")]
    public async Task<IActionResult> GetAppointmentJson(Guid appointmentId)
    {
        try
        {
            var appointment = await this._appointmentService.GetAppointmentAsync(id: appointmentId);
            return base.Json(data: appointment);
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [HttpGet]
    [Route("/[controller]/[action]/{appointmentId:Guid}")]
    public async Task<IActionResult> AppointmentInfo(Guid appointmentId)
    {
        try
        {
            var appointment = await this._appointmentService.GetAppointmentAsync(id: appointmentId);
            if (appointment == null)
            {
                return NotFound();
            }
            appointment!.Doctor = await this._doctorService.GetDoctorAsync(id: appointment.DoctorId) ?? new Doctor();
            appointment!.Patient = await this._patientService.GetPatientAsync(id: appointment.PatientId) ?? new Patient();
            appointment!.Room = await this._roomService.GetRoomAsync(id: appointment.RoomId) ?? new Room();

            return base.View(model: appointment);
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [HttpGet]
    [Authorize(Policy = "MediClubPolicyWorkRoles")]
    [Route("[action]", Name = "CreateAppointmentPage")]
    public async Task<IActionResult> Create()
    {
        var patients = await this._patientService.GetAllPatientsAsync();
        var doctors = await this._doctorService.GetAllDoctorsAsync();
        var rooms = await this._roomService.GetAllRoomsAsync();

        var model = new AppointmentViewModel
        {
            Doctors = doctors,
            Patients = patients,
            Rooms = rooms,
            Appointment = new Appointment()
        };

        return base.View(model);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost(Name = "CreateAppointmentApi")]
    public async Task<IActionResult> Create(AppointmentViewModel model)
    {
        try
        {
            var validatorResult = this._validator.Validate(instance: model.Appointment);
            if (!validatorResult.IsValid)
            {
                foreach (var error in validatorResult.Errors)
                {
                    base.ModelState.AddModelError(key: error.PropertyName, errorMessage: error.ErrorMessage);
                }

                var patients = await this._patientService.GetAllPatientsAsync();
                var doctors = await this._doctorService.GetAllDoctorsAsync();
                var rooms = await this._roomService.GetAllRoomsAsync();

                model = new AppointmentViewModel
                {
                    Doctors = doctors,
                    Patients = patients,
                    Rooms = rooms,
                    Appointment = new Appointment()
                };

                return base.View(viewName: "Create", model: model);
            }
            await this._appointmentService.CreateAppointmentAsync(newAppointment: model.Appointment);
            return base.RedirectToAction(actionName: "Index");
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<IActionResult> UpdateAppointment([FromBody] Appointment appointment)
    {
        try
        {
            await this._appointmentService.UpdateAppointmentAsync(id: appointment.Id, newAppointment: appointment);
            return base.Ok();
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{appointmentId:Guid}")]
    public async Task<IActionResult> DeleteAppointment(Guid appointmentId)
    {
        try
        {
            await this._appointmentService.DeleteAppointmentByIdAsync(id: appointmentId);
            return base.Ok();
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }
}