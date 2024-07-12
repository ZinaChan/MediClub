#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediClubApp.Models;

public class Specialization
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid DepartmentId { get; set; }

    [ForeignKey("DepartmentId")]
    public Department Department { get; set; }

    public ICollection<Doctor> Doctors { get; set; }
}