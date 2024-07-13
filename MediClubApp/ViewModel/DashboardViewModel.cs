#pragma warning disable CS8618
using MediClubApp.Models;

namespace MediClubApp.ViewModels;

public class DashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalDoctors { get; set; }
        public int TotalPatients { get; set; }
        public int TotalDepartments { get; set; }
        public int TotalSpecializations { get; set; }
        public int TotalRooms { get; set; }

    }