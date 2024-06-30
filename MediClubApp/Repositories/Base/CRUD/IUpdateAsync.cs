namespace MediClubApp.Repositories.Base.CRUD;
public interface IUpdateAsync<TEntity>
{
    Task UpdateAsync(Guid id, TEntity entity);
}