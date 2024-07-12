using MediClubApp.Models;
using MediClubApp.Repositories.Base;
using MediClubApp.Services.Base;

namespace MediClubApp.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;

    public RoomService(IRoomRepository roomRepository)
    {
        this._roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
    }
    public async Task CreateRoomAsync(Room newRoom)
    {
        if (newRoom is null)
        {
            throw new ArgumentNullException(nameof(newRoom));
        }

        await _roomRepository.CreateAsync(newRoom: newRoom);
    }
    public async Task<IEnumerable<Room>> GetAllRoomsAsync()
    {
        return await _roomRepository.GetAllAsync();
    }
    public async Task<Room?> GetRoomAsync(Guid id)
    {
        return await this._roomRepository.GetAsync(id: id);
    }
    public async Task UpdateRoomAsync(Guid id, Room newRoom)
    {

        if (newRoom is null || await this._roomRepository.GetAsync(id) is null)
        {
            throw new ArgumentNullException(nameof(newRoom));
        }

        await _roomRepository.UpdateAsync(id: id, newRoom: newRoom);
    }
    public async Task DeleteRoomByIdAsync(Guid id)
    {
        if (await this._roomRepository.GetAsync(id) is null)
        {
            throw new ArgumentNullException(nameof(Room));
        }

        await _roomRepository.DeleteByIdAsync(id: id);
    }
}