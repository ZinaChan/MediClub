#pragma warning disable CS8618

namespace MediClubApp.Models;

public class Log
{
    public int Id { get; set; }
    public string? Url { get; set; }
    public string? RequestBody { get; set; }
    public string? ResponsetBody { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime EndDate { get; set; }
    public int StatusCode { get; set; }
    public string? HttpMethod { get; set; }
}