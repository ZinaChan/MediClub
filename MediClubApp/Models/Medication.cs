#pragma warning disable CS8618

namespace MediClubApp.Models;

public class Medication
{
    public int MedicationId { get; set; }
    public string Name { get; set; }
    public string Dosage { get; set; }
    public string Instructions { get; set; }
}
