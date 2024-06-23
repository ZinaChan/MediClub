#pragma warning disable CS8618

namespace MediClubApp.Models;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Doctor> Doctors { get; set; }
    public ICollection<Specialization> Specializations { get; set; }
    public ICollection<Room> Rooms { get; set; }

}
