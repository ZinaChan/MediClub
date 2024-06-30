#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;

namespace MediClubApp.Models;

public class Department
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }

    public ICollection<Doctor> Doctors { get; set; }
    public ICollection<Specialization> Specializations { get; set; }
    public ICollection<Room> Rooms { get; set; }
}
