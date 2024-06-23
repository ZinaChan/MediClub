#pragma warning disable CS8618

namespace MediClubApp.Models;

public class Specialization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }