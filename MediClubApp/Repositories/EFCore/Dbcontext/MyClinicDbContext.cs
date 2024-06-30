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

        modelBuilder.ApplyConfiguration(new PatientConfiguration());
        modelBuilder.ApplyConfiguration(new RoomConfiguration());
        modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
        modelBuilder.ApplyConfiguration(new SpecializationConfiguration());
        modelBuilder.ApplyConfiguration(new DoctorConfiguration());
        modelBuilder.ApplyConfiguration(new AppointmentConfiguration());
        modelBuilder.ApplyConfiguration(new MedicalRecordConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());

        this.SeedDefaultData(modelBuilder);
    }

    private void SeedDefaultData(ModelBuilder modelBuilder)
    {
            var departmentId1 = Guid.NewGuid();
            var departmentId2 = Guid.NewGuid();
            var specializationId1 = Guid.NewGuid();
            var specializationId2 = Guid.NewGuid();
            var doctorId1 = Guid.NewGuid();
            var doctorId2 = Guid.NewGuid();
            var patientId1 = Guid.NewGuid();
            var patientId2 = Guid.NewGuid();
            var roomId1 = Guid.NewGuid();
            var roomId2 = Guid.NewGuid();
            var roomId3 = Guid.NewGuid();
            var roomId4 = Guid.NewGuid();
            var appointmentId1 = Guid.NewGuid();
            var appointmentId2 = Guid.NewGuid();
            var mrId1 = Guid.NewGuid();
            var mrId2 = Guid.NewGuid();

        modelBuilder.Entity<Department>().HasData(
            new Department { Id = departmentId1, Name = "Cardiology" },
            new Department { Id = departmentId2, Name = "Neurology" }
        );

        modelBuilder.Entity<Specialization>().HasData(
            new Specialization { Id = specializationId1, Name = "Cardiologist", DepartmentId = departmentId1 },
            new Specialization { Id = specializationId2, Name = "Neurologist", DepartmentId = departmentId2 }
        );

        modelBuilder.Entity<Patient>().HasData(
            new Patient
            {
                Id = patientId1,
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
                Id = patientId2,
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
                Id = doctorId1,
                FirstName = "Alice",
                LastName = "Johnson",
                DateOfBirth = new DateTime(1975, 2, 20),
                Gender = "Female",
                Email = "alice.johnson@example.com",
                PhoneNumber = "555-555-5557",
                DepartmentId = departmentId1,
                SpecializationId = specializationId1
            },
            new Doctor
            {
                Id = doctorId2,
                FirstName = "Bob",
                LastName = "Brown",
                DateOfBirth = new DateTime(1980, 8, 30),
                Gender = "Male",
                Email = "bob.brown@example.com",
                PhoneNumber = "555-555-5558",
                DepartmentId = departmentId1,
                SpecializationId = specializationId2
            }
        );

        modelBuilder.Entity<Room>().HasData(
            new Room { Id = roomId1, RoomNumber = "101", DepartmentId = departmentId1 },
            new Room { Id = roomId2, RoomNumber = "102", DepartmentId = departmentId2 },
            new Room { Id = roomId3, RoomNumber = "201", DepartmentId = departmentId1 },
            new Room { Id = roomId4, RoomNumber = "202", DepartmentId = departmentId2 }
        );

        modelBuilder.Entity<Appointment>().HasData(
            new Appointment
            {
                Id = appointmentId1,
                PatientId = patientId1,
                DoctorId = doctorId2,
                RoomId = roomId2,
                Date = new DateTime(2024, 7, 1),
                Time = new TimeSpan(10, 0, 0),
                Reason = "Routine Checkup"
            },
            new Appointment
            {
                Id = appointmentId2,
                PatientId = patientId2,
                DoctorId = doctorId1,
                RoomId = roomId1,
                Date = new DateTime(2024, 7, 2),
                Time = new TimeSpan(11, 0, 0),
                Reason = "Neurological Consultation"
            }
        );

        modelBuilder.Entity<MedicalRecord>().HasData(
            new MedicalRecord
            {
                Id = mrId1,
                PatientId = patientId1,
                DoctorId = doctorId2,
                Date = new DateTime(2024, 6, 1),
                Diagnosis = "Hypertension",
                Treatment = "Medication"
            },
            new MedicalRecord
            {
                Id = mrId2,
                PatientId = patientId2,
                DoctorId = doctorId1,
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
