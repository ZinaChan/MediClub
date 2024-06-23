using MediClubApp.Models;
using MediClubApp.Options;
using MediClubApp.Options.Base;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using MediClubApp.Repositories.EFCore.Dbcontext.Configuration;

namespace MediClubApp.Repositories.EFCore.Dbcontext;

public class MyClinicDbContext : DbContext
{
    private readonly IConnectionStringOption _connectionStringOption;

    public MyClinicDbContext(IOptionsSnapshot<MsSqlConnectionOption> sqlOptions)
    {
        this._connectionStringOption = sqlOptions.Value;
        // base.Database.EnsureDeleted();
        // base.Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(connectionString: _connectionStringOption.ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
        modelBuilder.ApplyConfiguration(new SpecializationConfiguration());
        modelBuilder.ApplyConfiguration(new MedicalRecordConfiguration());
        modelBuilder.ApplyConfiguration(new PatientConfiguration());
        modelBuilder.ApplyConfiguration(new RoomConfiguration());
        modelBuilder.ApplyConfiguration(new AppointmentConfiguration());

        this.SeedDefaultData(modelBuilder);
    }

    private void SeedDefaultData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>().HasData(
            new Department { Id = 1, Name = "Cardiology" },
            new Department { Id = 2, Name = "Neurology" }
        );

        modelBuilder.Entity<Specialization>().HasData(
            new Specialization { Id = 1, Name = "Cardiologist", DepartmentId = 1 },
            new Specialization { Id = 2, Name = "Neurologist", DepartmentId = 2 }
        );

        modelBuilder.Entity<Patient>().HasData(
            new Patient
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1980, 1, 1),
                Gender = "Male",
                Address = "123 Main St",
                PhoneNumber = "555-555-5555",
                Email = "john.doe@example.com"
            },
            new Patient
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Smith",
                DateOfBirth = new DateTime(1985, 5, 15),
                Gender = "Female",
                Address = "456 Elm St",
                PhoneNumber = "555-555-5556",
                Email = "jane.smith@example.com"
            }
        );

        modelBuilder.Entity<Doctor>().HasData(
            new Doctor
            {
                Id = 1,
                FirstName = "Alice",
                LastName = "Johnson",
                DateOfBirth = new DateTime(1975, 2, 20),
                Gender = "Female",
                Email = "alice.johnson@example.com",
                PhoneNumber = "555-555-5557",
                DepartmentId = 1,
                SpecializationId = 1
            },
            new Doctor
            {
                Id = 2,
                FirstName = "Bob",
                LastName = "Brown",
                DateOfBirth = new DateTime(1980, 8, 30),
                Gender = "Male",
                Email = "bob.brown@example.com",
                PhoneNumber = "555-555-5558",
                DepartmentId = 2,
                SpecializationId = 2
            }
        );

        modelBuilder.Entity<Room>().HasData(
            new Room { Id = 1, RoomNumber = "101", DepartmentId = 1 },
            new Room { Id = 2, RoomNumber = "102", DepartmentId = 1 },
            new Room { Id = 3, RoomNumber = "201", DepartmentId = 2 },
            new Room { Id = 4, RoomNumber = "202", DepartmentId = 2 }
        );

        modelBuilder.Entity<Appointment>().HasData(
            new Appointment
            {
                Id = 1,
                PatientId = 1,
                DoctorId = 1,
                RoomId = 1,
                Date = new DateTime(2024, 7, 1),
                Time = new TimeSpan(10, 0, 0),
                Reason = "Routine Checkup"
            },
            new Appointment
            {
                Id = 2,
                PatientId = 2,
                DoctorId = 2,
                RoomId = 3,
                Date = new DateTime(2024, 7, 2),
                Time = new TimeSpan(11, 0, 0),
                Reason = "Neurological Consultation"
            }
        );

        modelBuilder.Entity<MedicalRecord>().HasData(
            new MedicalRecord
            {
                Id = 1,
                PatientId = 1,
                DoctorId = 1,
                Date = new DateTime(2024, 6, 1),
                Diagnosis = "Hypertension",
                Treatment = "Medication"
            },
            new MedicalRecord
            {
                Id = 2,
                PatientId = 2,
                DoctorId = 2,
                Date = new DateTime(2024, 6, 15),
                Diagnosis = "Migraine",
                Treatment = "Rest and Medication"
            }
        );
    }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<MedicalRecord> MedicalRecords { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Specialization> Specializations { get; set; }
    public DbSet<Log> Logs { get; set; }
}
