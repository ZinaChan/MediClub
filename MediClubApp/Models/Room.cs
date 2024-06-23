#pragma warning disable CS8618

namespace MediClubApp.Models;

public class Room
{
    public int Id { get; set; }
    public string RoomNumber { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; }

    public ICollection<Appointment> Appointments { get; set; }

}
