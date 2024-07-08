#pragma warning disable CS8618
using MediClubApp.Models;

namespace MediClubApp.ViewModels;

public class DoctorViewModel
{
    public Doctor Doctor { get; set; }
    public IEnumerable<Department> Departments { get; set; }
    public IEnumerable<Specialization> Specializations { get; set; }
    public IEnumerable<Doctor> Doctors { get; set; }
}
