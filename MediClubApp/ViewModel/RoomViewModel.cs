#pragma warning disable CS8618
using MediClubApp.Models;

namespace MediClubApp.ViewModels;

public class RoomViewModel
{
    public Room Room { get; set; }
    public IEnumerable<Department> Departments { get; set; }
    public IEnumerable<Room> Rooms { get; set; }
}