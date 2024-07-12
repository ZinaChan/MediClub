namespace MediClubApp.Repositories.Base.CRUD;

public interface ICreateAsync<TEntity>
{
    Task CreateAsync(TEntity entity);
}
