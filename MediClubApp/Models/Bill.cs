#pragma warning disable CS8618

namespace MediClubApp.Models;

public class Bill
{
    public int BillId { get; set; }
    public int VisitId { get; set; }
    public decimal AmountDue { get; set; }
    public DateTime DueDate { get; set; }
    public string PaymentStatus { get; set; }
}