namespace MediClubApp.Repositories.Base;
public interface IDeleteAsync<TEntity>
{
    Task DeleteByIdAsync(int id);
}