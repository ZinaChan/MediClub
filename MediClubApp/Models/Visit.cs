#pragma warning disable CS8618

namespace MediClubApp.Models;

public class Visit
{
    public int VisitId { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime DateOfVisit { get; set; }
    public string Diagnosis { get; set; }
    public string TreatmentPlan { get; set; }
    public string Notes { get; set; }
}
