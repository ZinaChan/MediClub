#pragma warning disable CS1998

using MediClubApp.Models;
using MediClubApp.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using MediClubApp.Repositories.EFCore.Dbcontext;

namespace MediClubApp.Repositories.EFCore;

public class RoomEFCoreRepository : IRoomRepository
{
    private readonly MyClinicDbContext _clinicDbContext;
    public RoomEFCoreRepository(MyClinicDbContext dbContext)
    {
        this._clinicDbContext = dbContext;
    }

    public async Task CreateAsync(Room newRoom)
    {
        await this._clinicDbContext.Rooms.AddAsync(entity: newRoom);
        await this._clinicDbContext.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(int id)
    {
        var oldRoom = await this._clinicDbContext.Rooms.FirstOrDefaultAsync(d => d.Id == id);

        if (oldRoom is null) return;

        this._clinicDbContext.Rooms.Remove(entity: oldRoom);
        await this._clinicDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Room>> GetAllAsync()
    {
        var rooms = await this._clinicDbContext.Rooms.Include(r => r.Department).ToListAsync();

        return rooms;
    }

    public Task<Room?> GetAsync(int id)
    {
        return this._clinicDbContext.Rooms.FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task UpdateAsync(int id, Room newRoom)
    {
        var oldRoom = await this._clinicDbContext.Rooms.FirstOrDefaultAsync(d => d.Id == id);
        if (oldRoom is null) return;

         oldRoom.RoomNumber = newRoom.RoomNumber;
        oldRoom.DepartmentId = newRoom.DepartmentId;

        this._clinicDbContext.Update(oldRoom);
        await this._clinicDbContext.SaveChangesAsync();
    }
}