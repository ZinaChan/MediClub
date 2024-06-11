#pragma warning disable CS8618

namespace MediClubApp.Models;

public class Doctor
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; } 
    public string Email { get; set; } 
}
