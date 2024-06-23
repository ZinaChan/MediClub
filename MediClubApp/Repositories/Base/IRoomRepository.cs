#pragma warning disable CS0108

using MediClubApp.Models;
using MediClubApp.Repositories.Base.CRUD;

namespace MediClubApp.Repositories.Base;

public interface IRoomRepository :  ICreateAsync<Room>, IGetAsync<Room>, IGetAllAsync<Room>, IUpdateAsync<Room>, IDeleteAsync<Room>
{
    Task<Room?> GetAsync(int id);
    Task<IEnumerable<Room>> GetAllAsync();
    Task CreateAsync(Room newRoom);
    Task UpdateAsync(int id, Room newRoom);
    Task DeleteByIdAsync(int id);
}
