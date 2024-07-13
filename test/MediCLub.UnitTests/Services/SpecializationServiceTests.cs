using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using MediClubApp.Models;
using MediClubApp.Repositories.Base;
using MediClubApp.Services;

namespace MediCLub.UnitTests.Services;
public class SpecializationServiceTests
{
    private Mock<ISpecializationRepository> _mockSpecializationRepository;
    private SpecializationService _specializationService;

    public SpecializationServiceTests()
    {
        _mockSpecializationRepository = new Mock<ISpecializationRepository>();
        _specializationService = new SpecializationService(_mockSpecializationRepository.Object);
    }

    [Fact]
    public async Task CreateSpecializationAsync_ThrowsArgumentNullException_WhenSpecializationIsNull()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await _specializationService.CreateSpecializationAsync(null));
    }

    [Fact]
    public async Task CreateSpecializationAsync_CallsRepository_WithCorrectSpecialization()
    {
        var newSpecialization = new Specialization { Name = "Test Specialization" };

        await _specializationService.CreateSpecializationAsync(newSpecialization);

        _mockSpecializationRepository.Verify(repo => repo.CreateAsync(It.Is<Specialization>(s => s == newSpecialization)), Times.Once);
    }
    [Fact]
    public async Task GetAllSpecializationsAsync_ReturnsEmptyList_WhenNoSpecializationsExist()
    {
        _mockSpecializationRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(Enumerable.Empty<Specialization>());
        var specializations = await _specializationService.GetAllSpecializationsAsync();
        Assert.Empty(specializations);
    }

    [Fact]
    public async Task GetAllSpecializationsAsync_ReturnsSpecializations_FromRepository()
    {
        var specializations = new List<Specialization>() { new Specialization { Name = "Specialization 1" }, new Specialization { Name = "Specialization 2" } };
        _mockSpecializationRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(specializations);
        var returnedSpecializations = await _specializationService.GetAllSpecializationsAsync();
        Assert.Equal(specializations, returnedSpecializations);
    }
    [Fact]
    public async Task GetSpecializationAsync_ReturnsNull_WhenSpecializationNotFound()
    {
        _mockSpecializationRepository.Setup(repo => repo.GetAsync(It.IsAny<Guid>())).ReturnsAsync((Specialization)null);
        var specialization = await _specializationService.GetSpecializationAsync(Guid.NewGuid());
        Assert.Null(specialization);
    }

    [Fact]
    public async Task GetSpecializationAsync_ReturnsSpecialization_FromRepository()
    {
        var expectedSpecialization = new Specialization { Name = "Existing Specialization" };
        _mockSpecializationRepository.Setup(repo => repo.GetAsync(It.IsAny<Guid>())).ReturnsAsync(expectedSpecialization);
        var specialization = await _specializationService.GetSpecializationAsync(Guid.NewGuid());
        Assert.Equal(expectedSpecialization, specialization);
    }
    [Fact]
    public async Task UpdateSpecializationAsync_ThrowsArgumentNullException_WhenSpecializationIsNull()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await _specializationService.UpdateSpecializationAsync(Guid.NewGuid(), null));
    }

    [Fact]
    public async Task UpdateSpecializationAsync_ThrowsArgumentNullException_WhenSpecializationNotFound()
    {
        var newSpecialization = new Specialization { Name = "Updated Specialization" };
        _mockSpecializationRepository.Setup(repo => repo.GetAsync(It.IsAny<Guid>())).ReturnsAsync((Specialization)null);
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await _specializationService.UpdateSpecializationAsync(Guid.NewGuid(), newSpecialization));
    }
}
