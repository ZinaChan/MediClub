#pragma warning disable CS8618

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MediClubApp.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }
    public string? AvatarUrl { set; get; }
    
    public string Email { set; get; }
    [MinLength(8)]
    [MaxLength(20)]
    [PasswordPropertyText]
    public string Password { set; get; }

    public string Role { get; set; }

    [MinLength(2)]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [MinLength(2)]
    [MaxLength(50)]
    public string LastName { get; set; }

    public string Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }

}
