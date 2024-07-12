namespace MediClubApp.Repositories.Base.CRUD;
public interface IGetAllAsync<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync();
}
