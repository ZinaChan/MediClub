#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;

namespace MediClubApp.Models;

public class Log
{
    [Key]
    public Guid Id { get; set; }
    public string? Url { get; set; }
    public string? RequestBody { get; set; }
    public string? ResponsetBody { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime EndDate { get; set; }
    public int StatusCode { get; set; }
    public string? HttpMethod { get; set; }
}