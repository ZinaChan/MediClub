#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;

namespace MediClubApp.Models;

public class User
{ 
    [Key]
    public Guid Id { get; set; }

    [MinLength(8)]
    [MaxLength(20)]
    public string Login { set; get; }

    [MinLength(8)]
    [MaxLength(20)]
    public string Password { set; get; }

    [EmailAddress(ErrorMessage = "Invalid Email Adress.")]
    public string Email { set; get; }

    [MinLength(8)]
    [MaxLength(20)]
    public string Username { set; get; }
    public string? AvatarUrl { set; get; }
    
    public UserRole Role { get; set; }  
}
