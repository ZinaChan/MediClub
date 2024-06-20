namespace MediClubApp.Repositories.Base.CRUD;
public interface IUpdateAsync<TEntity>
{
    Task UpdateAsync(int id, TEntity entity);
}