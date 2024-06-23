using MediClubApp.Models;
namespace MediClubApp.Services.Base;

public interface IRoomService
{
    Task<Room?> GetRoomAsync(int id);
    Task<IEnumerable<Room>> GetAllRoomsAsync();
    Task CreateRoomAsync(Room newRoom);
    Task UpdateRoomAsync(int id, Room newRoom);
    Task DeleteRoomByIdAsync(int id);
}