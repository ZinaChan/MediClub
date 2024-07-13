using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using MediClubApp.Models;
using MediClubApp.Repositories.Base;
using MediClubApp.Services;

namespace MediCLub.UnitTests.Services;
public class RoomServiceTests
{
    private Mock<IRoomRepository> _mockRoomRepository;
    private RoomService _roomService;

    public RoomServiceTests()
    {
        _mockRoomRepository = new Mock<IRoomRepository>();
        _roomService = new RoomService(_mockRoomRepository.Object);
    }


    [Fact]
    public async Task GetRoomAsync_ReturnsNull_WhenRoomNotFound()
    {
        _mockRoomRepository.Setup(repo => repo.GetAsync(It.IsAny<Guid>())).ReturnsAsync((Room)null);
        var room = await _roomService.GetRoomAsync(Guid.NewGuid());
        Assert.Null(room);
    }

    [Fact]
    public async Task GetRoomAsync_ReturnsRoom_FromRepository()
    {
        var expectedRoom = new Room { RoomNumber = "101" };
        _mockRoomRepository.Setup(repo => repo.GetAsync(It.IsAny<Guid>())).ReturnsAsync(expectedRoom);
        var returnedRoom = await _roomService.GetRoomAsync(Guid.NewGuid());
        Assert.Equal(expectedRoom, returnedRoom);
    }
}