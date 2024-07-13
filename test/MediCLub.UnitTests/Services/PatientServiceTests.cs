using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using MediClubApp.Models;
using MediClubApp.Repositories.Base;
using MediClubApp.Services;
namespace MediCLub.UnitTests.Services;
public class PatientServiceTests
{
    private Mock<IPatientRepository> _mockPatientRepository;
    private PatientService _patientService;

    public PatientServiceTests()
    {
        _mockPatientRepository = new Mock<IPatientRepository>();
        _patientService = new PatientService(_mockPatientRepository.Object);
    }


    [Fact]
    public async Task CreatePatientAsync_ThrowsArgumentNullException_WhenNewPatientIsNull()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await _patientService.CreatePatientAsync(null));
    }

    [Fact]
    public async Task GetPatientsByDoctorAsync_ReturnsEmptyList_WhenNoPatientsFound()
    {
        _mockPatientRepository.Setup(repo => repo.GetPatientsByDoctorAsync(It.IsAny<Guid>())).ReturnsAsync(Enumerable.Empty<Patient>());
        var patients = await _patientService.GetPatientsByDoctorAsync(Guid.NewGuid());
        Assert.Empty(patients);
    }

    [Fact]
    public async Task GetPatientsByDoctorAsync_ReturnsPatients_FromRepository()
    {
        var expectedPatients = new List<Patient>() { new Patient { FirstName = "Patient 1" }, new Patient { FirstName = "Patient 2" } };
        _mockPatientRepository.Setup(repo => repo.GetPatientsByDoctorAsync(It.IsAny<Guid>())).ReturnsAsync(expectedPatients);
        var returnedPatients = await _patientService.GetPatientsByDoctorAsync(Guid.NewGuid());
        Assert.Equal(expectedPatients, returnedPatients);
    }
}
