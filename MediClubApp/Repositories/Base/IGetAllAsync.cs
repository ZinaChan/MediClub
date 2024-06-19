namespace MediClubApp.Repositories.Base;
public interface IGetAllAsync<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync();
}
