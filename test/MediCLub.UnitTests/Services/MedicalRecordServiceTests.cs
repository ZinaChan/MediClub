using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using MediClubApp.Models;
using MediClubApp.Repositories.Base;
using MediClubApp.Services;

namespace MediCLub.UnitTests.Services;
public class MedicalRecordServiceTests
{
    private Mock<IMedicalRecordRepository> _mockMedicalRecordRepository;
    private MedicalRecordService _medicalRecordService;

    public MedicalRecordServiceTests()
    {
        _mockMedicalRecordRepository = new Mock<IMedicalRecordRepository>();
        _medicalRecordService = new MedicalRecordService(_mockMedicalRecordRepository.Object);
    }

    [Fact]
    public async Task GetMedicalRecordsForPatientAsync_ReturnsEmptyList_WhenNoRecordsFound()
    {
        _mockMedicalRecordRepository.Setup(repo => repo.GetMedicalRecordsForPatientAsync(It.IsAny<Guid>())).ReturnsAsync(Enumerable.Empty<MedicalRecord>());
        var records = await _medicalRecordService.GetMedicalRecordsForPatientAsync(Guid.NewGuid());
        Assert.Empty(records);
    }

    [Fact]
    public async Task GetMedicalRecordsForPatientAsync_ReturnsRecords_FromRepository()
    {
        var expectedRecords = new List<MedicalRecord>() { new MedicalRecord { PatientId = Guid.NewGuid() }, new MedicalRecord { PatientId = Guid.NewGuid() } };
        _mockMedicalRecordRepository.Setup(repo => repo.GetMedicalRecordsForPatientAsync(It.IsAny<Guid>())).ReturnsAsync(expectedRecords);
        var returnedRecords = await _medicalRecordService.GetMedicalRecordsForPatientAsync(Guid.NewGuid());
        Assert.Equal(expectedRecords, returnedRecords);
    }

    [Fact]
    public async Task GetMedicalRecordsForDoctorAsync_ReturnsEmptyList_WhenNoRecordsFound()
    {
        _mockMedicalRecordRepository.Setup(repo => repo.GetMedicalRecordsForDoctorAsync(It.IsAny<Guid>())).ReturnsAsync(Enumerable.Empty<MedicalRecord>());
        var records = await _medicalRecordService.GetMedicalRecordsForDoctorAsync(Guid.NewGuid());
        Assert.Empty(records);
    }

    [Fact]
    public async Task GetMedicalRecordsForDoctorAsync_ReturnsRecords_FromRepository()
    {
        var expectedRecords = new List<MedicalRecord>() { new MedicalRecord { DoctorId = Guid.NewGuid() }, new MedicalRecord { DoctorId = Guid.NewGuid() } };
        _mockMedicalRecordRepository.Setup(repo => repo.GetMedicalRecordsForDoctorAsync(It.IsAny<Guid>())).ReturnsAsync(expectedRecords);
        var returnedRecords = await _medicalRecordService.GetMedicalRecordsForDoctorAsync(Guid.NewGuid());
        Assert.Equal(expectedRecords, returnedRecords);
    }
}
