using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using MediClubApp.Models;
using MediClubApp.Repositories.Base;
using MediClubApp.Services;
namespace MediCLub.UnitTests.Services;
public class UserServiceTests
{
    private Mock<IUserRepository> _mockUserRepository;
    private UserService _userService;

    public UserServiceTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _userService = new UserService(_mockUserRepository.Object);
    }


    [Fact]
    public async Task GetUserAsync_ById_ReturnsNull_WhenUserNotFound()
    {
        _mockUserRepository.Setup(repo => repo.GetAsync(It.IsAny<Guid>())).ReturnsAsync((User)null);
        var user = await _userService.GetUserAsync(Guid.NewGuid());
        Assert.Null(user);
    }

    [Fact]
    public async Task GetUserAsync_ById_ReturnsUser_FromRepository()
    {
        var expectedUser = new User { FirstName = "Mary" };
        _mockUserRepository.Setup(repo => repo.GetAsync(It.IsAny<Guid>())).ReturnsAsync(expectedUser);
        var returnedUser = await _userService.GetUserAsync(Guid.NewGuid());
        Assert.Equal(expectedUser, returnedUser);
    }


    [Fact]
    public async Task UpdateUserAsync_ThrowsArgumentNullException_WhenUserIsNull()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await _userService.UpdateUserAsync(Guid.NewGuid(), null));
    }

    [Fact]
    public async Task UpdateUserAsync_ThrowsArgumentNullException_WhenUserNotFound()
    {
        _mockUserRepository.Setup(repo => repo.GetAsync(It.IsAny<Guid>())).ReturnsAsync((User)null);
        var newUser = new User { FirstName = "Updated Name" };
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await _userService.UpdateUserAsync(Guid.NewGuid(), newUser));
    }
}
