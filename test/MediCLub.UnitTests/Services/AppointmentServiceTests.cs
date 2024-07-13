using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using MediClubApp.Models;
using MediClubApp.Repositories.Base;
using MediClubApp.Services;

public class AppointmentServiceTests
{
    private readonly Mock<IAppointmentRepository> _mockRepository;
    private readonly AppointmentService _appointmentService;

    public AppointmentServiceTests()
    {
        _mockRepository = new Mock<IAppointmentRepository>();
        _appointmentService = new AppointmentService(_mockRepository.Object);
    }

    [Fact]
    public async Task CreateAppointmentAsync_ValidAppointment_CallsRepositoryCreate()
    {
        var appointment = new Appointment { Id = Guid.NewGuid()};
        await _appointmentService.CreateAppointmentAsync(appointment);

        _mockRepository.Verify(repo => repo.CreateAsync(appointment), Times.Once);
    }

    [Fact]
    public async Task CreateAppointmentAsync_NullAppointment_ThrowsArgumentNullException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _appointmentService.CreateAppointmentAsync(null));
    }

    [Fact]
    public async Task GetAllAppointmentsAsync_ReturnsAllAppointments()
    {
        var appointments = new List<Appointment>
            {
                new Appointment { Id = Guid.NewGuid() },
                new Appointment { Id = Guid.NewGuid()}
            };
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(appointments);

        var result = await _appointmentService.GetAllAppointmentsAsync();

        Assert.Equal(appointments.Count, result.Count());
        Assert.Equal(appointments.First().Id, result.First().Id);
    }

    [Fact]
    public async Task GetAppointmentAsync_ExistingId_ReturnsAppointment()
    {
        var appointmentId = Guid.NewGuid();
        var appointment = new Appointment { Id = appointmentId};
        _mockRepository.Setup(repo => repo.GetAsync(appointmentId)).ReturnsAsync(appointment);
        var result = await _appointmentService.GetAppointmentAsync(appointmentId);
        Assert.Equal(appointmentId, result.Id);
    }

    [Fact]
    public async Task GetAppointmentAsync_NonExistingId_ReturnsNull()
    {
        var noExistingId = Guid.NewGuid();
        _mockRepository.Setup(repo => repo.GetAsync(noExistingId)).ReturnsAsync((Appointment)null);

        var result = await _appointmentService.GetAppointmentAsync(noExistingId);

        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateAppointmentAsync_NullAppointment_ThrowsArgumentNullException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _appointmentService.UpdateAppointmentAsync(Guid.NewGuid(), null));
    }

    [Fact]
    public async Task DeleteAppointmentByIdAsync_ExistingId_CallsRepositoryDelete()
    {
        var appointmentId = Guid.NewGuid();
        _mockRepository.Setup(repo => repo.GetAsync(appointmentId)).ReturnsAsync(new Appointment { Id = appointmentId});

        await _appointmentService.DeleteAppointmentByIdAsync(appointmentId);

        _mockRepository.Verify(repo => repo.DeleteByIdAsync(appointmentId), Times.Once);
    }

    [Fact]
    public async Task DeleteAppointmentByIdAsync_NonExistingId_ThrowsArgumentNullException()
    {
        var nonExistingId = Guid.NewGuid();
        _mockRepository.Setup(repo => repo.GetAsync(nonExistingId)).ReturnsAsync((Appointment)null);

        await Assert.ThrowsAsync<ArgumentNullException>(() => _appointmentService.DeleteAppointmentByIdAsync(nonExistingId));
    }

    [Fact]
    public async Task GetAppointmentsForDoctorAsync_ValidDoctorId_ReturnsAppointments()
    {
        var doctorId = Guid.NewGuid();
        var appointments = new List<Appointment>
            {
                new Appointment { Id = Guid.NewGuid(), DoctorId = doctorId, },
                new Appointment { Id = Guid.NewGuid(), DoctorId = doctorId, }
            };
        _mockRepository.Setup(repo => repo.GetAppointmentsForDoctorAsync(doctorId)).ReturnsAsync(appointments);

        var result = await _appointmentService.GetAppointmentsForDoctorAsync(doctorId);

        Assert.Equal(appointments.Count, result.Count());
        Assert.Equal(doctorId, result.First().DoctorId);
    }

    [Fact]
    public async Task GetAppointmentsForPatientAsync_ValidPatientId_ReturnsAppointments()
    {
        var patientId = Guid.NewGuid();
        var appointments = new List<Appointment>
            {
                new Appointment { Id = Guid.NewGuid(), PatientId = patientId, },
                new Appointment { Id = Guid.NewGuid(), PatientId = patientId, }
            };
        _mockRepository.Setup(repo => repo.GetAppointmentsForPatientAsync(patientId)).ReturnsAsync(appointments);

        var result = await _appointmentService.GetAppointmentsForPatientAsync(patientId);

        Assert.Equal(appointments.Count, result.Count());
        Assert.Equal(patientId, result.First().PatientId);
    }
}
