using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using MediClubApp.Models;
using MediClubApp.Repositories.Base;
using MediClubApp.Services;

namespace MediCLub.UnitTests.Services
{
    public class DoctorServiceTests
    {
        private Mock<IDoctorRepository> _mockDoctorRepository;
        private DoctorService _doctorService;

        public DoctorServiceTests()
        {
            _mockDoctorRepository = new Mock<IDoctorRepository>();
            _doctorService = new DoctorService(_mockDoctorRepository.Object);
        }

        [Fact]
        public async Task GetDoctorsByDepartmentAsync_ReturnsEmptyList_WhenNoDoctorsFound()
        {
            _mockDoctorRepository.Setup(repo => repo.GetDoctorsByDepartmentAsync(It.IsAny<Guid>())).ReturnsAsync(new List<Doctor>());
            var doctors = await _doctorService.GetDoctorsByDepartmentAsync(Guid.NewGuid());
            Assert.Empty(doctors);
        }

        [Fact]
        public async Task GetDoctorsByDepartmentAsync_ReturnsDoctors_FromRepository()
        {
            var expectedDoctors = new List<Doctor>() { new Doctor { FirstName = "Doctor 1" }, new Doctor { FirstName = "Doctor 2" } };
            _mockDoctorRepository.Setup(repo => repo.GetDoctorsByDepartmentAsync(It.IsAny<Guid>())).ReturnsAsync(expectedDoctors);
            var returnedDoctors = await _doctorService.GetDoctorsByDepartmentAsync(Guid.NewGuid());
            Assert.Equal(expectedDoctors, returnedDoctors);
        }

        [Fact]
        public async Task GetDoctorsBySpecializationAsync_ReturnsEmptyList_WhenNoDoctorsFound()
        {
            _mockDoctorRepository.Setup(repo => repo.GetDoctorsBySpecializationAsync(It.IsAny<Guid>())).ReturnsAsync(new List<Doctor>());
            var doctors = await _doctorService.GetDoctorsBySpecializationAsync(Guid.NewGuid());
            Assert.Empty(doctors);
        }

        [Fact]
        public async Task GetDoctorsBySpecializationAsync_ReturnsDoctors_FromRepository()
        {
            var expectedDoctors = new List<Doctor>() { new Doctor { FirstName = "Doctor 3" }, new Doctor { FirstName = "Doctor 4" } };
            _mockDoctorRepository.Setup(repo => repo.GetDoctorsBySpecializationAsync(It.IsAny<Guid>())).ReturnsAsync(expectedDoctors);
            var returnedDoctors = await _doctorService.GetDoctorsBySpecializationAsync(Guid.NewGuid());
            Assert.Equal(expectedDoctors, returnedDoctors);
        }

        [Fact]
        public async Task GetDoctorsByPatientAsync_ReturnsEmptyList_WhenNoDoctorsFound()
        {
            _mockDoctorRepository.Setup(repo => repo.GetDoctorsByPatientAsync(It.IsAny<Guid>())).ReturnsAsync(Enumerable.Empty<Doctor>());
            var doctors = await _doctorService.GetDoctorsByPatientAsync(Guid.NewGuid());
            Assert.Empty(doctors);
        }

        [Fact]
        public async Task GetDoctorsByPatientAsync_ReturnsDoctors_FromRepository()
        {
            var expectedDoctors = new List<Doctor>() { new Doctor { FirstName = "Doctor 5" }, new Doctor { FirstName = "Doctor 6" } };
            _mockDoctorRepository.Setup(repo => repo.GetDoctorsByPatientAsync(It.IsAny<Guid>())).ReturnsAsync(expectedDoctors);
            var returnedDoctors = await _doctorService.GetDoctorsByPatientAsync(Guid.NewGuid());
            Assert.Equal(expectedDoctors, returnedDoctors);
        }
    }
}