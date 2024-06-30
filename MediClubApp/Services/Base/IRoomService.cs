using MediClubApp.Models;
namespace MediClubApp.Services.Base;

public interface IRoomService
{
    Task<Room?> GetRoomAsync(Guid id);
    Task<IEnumerable<Room>> GetAllRoomsAsync();
    Task CreateRoomAsync(Room newRoom);
    Task UpdateRoomAsync(Guid id, Room newRoom);
    Task DeleteRoomByIdAsync(Guid id);
}