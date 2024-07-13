using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using MediClubApp.Models;
using MediClubApp.Repositories.Base;
using MediClubApp.Services;

namespace MediCLub.UnitTests.Services;
public class LogServiceTests
{
    private Mock<ILogRepository> _mockLogRepository;
    private LogService _logService;

    public LogServiceTests()
    {
        _mockLogRepository = new Mock<ILogRepository>();
        _logService = new LogService(_mockLogRepository.Object);
    }

    [Fact]
    public async Task CreateLogAsync_ThrowsArgumentNullException_WhenNewLogIsNull()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await _logService.CreateLogAsync(null));
    }
  
    [Fact]
    public async Task GetAllLogsAsync_ReturnsEmptyList_WhenNoLogsExist()
    {
        _mockLogRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(Enumerable.Empty<Log>());
        var logs = await _logService.GetAllLogsAsync();
        Assert.Empty(logs);
    }
 
}
