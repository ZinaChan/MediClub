using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using MediClubApp.Models;
using MediClubApp.Repositories.Base;
using MediClubApp.Services;

namespace MediCLub.UnitTests.Services;

public class DepartmentServiceTests
{
    private Mock<IDepartmentRepository> _mockDepartmentRepository;
    private DepartmentService _departmentService;

    public DepartmentServiceTests()
    {
        _mockDepartmentRepository = new Mock<IDepartmentRepository>();
        _departmentService = new DepartmentService(_mockDepartmentRepository.Object);
    }

    [Fact]
    public async Task CreateDepartmentAsync_ThrowsArgumentNullException_WhenDepartmentIsNull()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await _departmentService.CreateDepartmentAsync(null));
    }

    [Fact]
    public async Task CreateDepartmentAsync_CallsRepository_WithCorrectDepartment()
    {
        var newDepartment = new Department { Name = "Test Department" };
        await _departmentService.CreateDepartmentAsync(newDepartment);
        _mockDepartmentRepository.Verify(repo => repo.CreateAsync(It.Is<Department>(d => d == newDepartment)), Times.Once);
    }
    [Fact]
    public async Task GetAllDepartmentsAsync_ReturnsEmptyList_WhenNoDepartmentsExist()
    {
        _mockDepartmentRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(Enumerable.Empty<Department>());
        var departments = await _departmentService.GetAllDepartmentsAsync();
        Assert.Empty(departments);
    }

    [Fact]
    public async Task GetAllDepartmentsAsync_ReturnsDepartments_FromRepository()
    {
        var departments = new List<Department>() { new Department { Name = "Department 1" }, new Department { Name = "Department 2" } };
        _mockDepartmentRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(departments);
        var returnedDepartments = await _departmentService.GetAllDepartmentsAsync();
        Assert.Equal(departments, returnedDepartments);
    }

    [Fact]
    public async Task GetDepartmentAsync_ReturnsDepartment_FromRepository()
    {
        var expectedDepartment = new Department { Name = "Existing Department" };
        _mockDepartmentRepository.Setup(repo => repo.GetAsync(It.IsAny<Guid>())).ReturnsAsync(expectedDepartment);
        var department = await _departmentService.GetDepartmentAsync(Guid.NewGuid());
        Assert.Equal(expectedDepartment, department);
    }

    [Fact]
    public async Task UpdateDepartmentAsync_ThrowsArgumentNullException_WhenDepartmentIsNull()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await _departmentService.UpdateDepartmentAsync(Guid.NewGuid(), null));
    }

    [Fact]
    public async Task UpdateDepartmentAsync_ThrowsArgumentNullException_WhenDepartmentNotFound()
    {
        var newDepartment = new Department { Name = "Updated Department" };
        _mockDepartmentRepository.Setup(repo => repo.GetAsync(It.IsAny<Guid>())).ReturnsAsync((Department)null);
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await _departmentService.UpdateDepartmentAsync(Guid.NewGuid(), newDepartment));
    }

}