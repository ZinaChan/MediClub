namespace MediClubApp.Repositories.Base;

public interface IGetAsync<TEntity>
{
    Task<TEntity?> GetAsync(int id);
}