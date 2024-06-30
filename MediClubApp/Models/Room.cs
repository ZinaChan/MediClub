#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediClubApp.Models;

public class Room
{
    [Key]
    public Guid Id { get; set; }
    public string RoomNumber { get; set; }
    public Guid DepartmentId { get; set; }

    [ForeignKey("DepartmentId")]
    public Department Department { get; set; }

    public ICollection<Appointment> Appointments { get; set; }

}
