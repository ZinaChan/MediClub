#pragma warning disable CS8618
using MediClubApp.Models;

namespace MediClubApp.ViewModels;

public class DashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalDepartments { get; set; }
        public int TotalRooms { get; set; }
        public int TotalSpecializations { get; set; }
        public int TotalAppointments { get; set; }
        public IEnumerable<Appointment> Appointments { get; set; }
        public IEnumerable<User> Users { get; set; }
    }