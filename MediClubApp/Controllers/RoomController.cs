using FluentValidation;
using MediClubApp.Models;
using MediClubApp.Services.Base;
using MediClubApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MediClubApp.Controllers;

[Route("[controller]")]
public class RoomController : Controller
{
    private readonly IValidator<Room> _validator;
    private readonly IRoomService _roomService;
    private readonly IDepartmentService _departmenttService;

    public RoomController(IValidator<Room> validator, IRoomService roomService, IDepartmentService departmenttService)
    {
        this._validator = validator;
        this._roomService = roomService;
        this._departmenttService = departmenttService;
    }

    [HttpGet]
    [Route("/[controller]")]
    public async Task<IActionResult> Index()
    {
        var model = new RoomViewModel
        {
            Departments = await this._departmenttService.GetAllDepartmentsAsync(),
            Rooms = await this._roomService.GetAllRoomsAsync()
        }; 
        return base.View(model);
    }

    [HttpGet]
    [Route("Json")]
    public async Task<IActionResult> GetAllRoomsJson()
    {
        try
        {
            var rooms = await this._roomService.GetAllRoomsAsync();
            foreach (var room in rooms)
            {
                room.Department = null!;
            }
            return base.Json(data: rooms);
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [HttpGet]
    [Route("Json/{roomId:int}")]
    public async Task<IActionResult> GetRoomJson(int roomId)
    {
        try
        {
            var room = await this._roomService.GetRoomAsync(id: roomId);
            return base.Json(data: room);
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [HttpGet]
    [Route("/[controller]/{roomId:int}")]
    public async Task<IActionResult> RoomInfo(int roomId)
    {
        try
        {
            var room = await this._roomService.GetRoomAsync(id: roomId);
            room!.Department = await this._departmenttService.GetDepartmentAsync(id: room.DepartmentId) ?? new Department();

            return base.View(model: room);
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [HttpGet]
    [Route("[action]", Name = "CreateRoomPage")]
    public async Task<IActionResult> Create()
    {
        var departments = await this._departmenttService.GetAllDepartmentsAsync();

        var model = new RoomViewModel
        {
            Departments = departments,
            Room = new Room()
        };

        return base.View(model);
    }

    [HttpPost(Name = "CreateRoomApi")]
    public async Task<IActionResult> Create(RoomViewModel model)
    {
        try
        {
            var validatorResult = this._validator.Validate(instance: model.Room);
            if (!validatorResult.IsValid)
            {
                foreach (var error in validatorResult.Errors)
                {
                    base.ModelState.AddModelError(key: error.PropertyName, errorMessage: error.ErrorMessage);
                }

                var departments = await this._departmenttService.GetAllDepartmentsAsync();
                model = new RoomViewModel
                {
                    Departments = departments,
                    Room = new Room()
                };
                return base.View(viewName: "Create", model: model);
            }
            await this._roomService.CreateRoomAsync(newRoom: model.Room);
            return base.RedirectToAction(actionName: "Index");
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateRoom([FromBody] Room room)
    {
        try
        {
            await this._roomService.UpdateRoomAsync(id: room.Id, newRoom: room);
            return base.Ok();
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }

    [HttpDelete("{roomId:int}")]
    public async Task<IActionResult> DeleteRoom(int roomId)
    {
        try
        {
            await this._roomService.DeleteRoomByIdAsync(id: roomId);
            return base.Ok();
        }
        catch (System.Exception ex)
        {
            return base.StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: ex.Message);
        }
    }
}