namespace MediClubApp.Repositories.Base.CRUD;

public interface IGetAsync<TEntity>
{
    Task<TEntity?> GetAsync(Guid id);
}
