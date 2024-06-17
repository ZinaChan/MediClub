namespace MediClubApp.Repositories.Base;
public interface IDeleteAsync<TEntity>
{
    Task DeleteAsync(TEntity entity);
}