#pragma warning disable CS8618
using MediClubApp.Models;

namespace MediClubApp.ViewModels;

public class SpecializationViewModel
{
    public Specialization Specialization { get; set; }
    public IEnumerable<Department> Departments { get; set; }
    public IEnumerable<Specialization> Specializations { get; set; }
}