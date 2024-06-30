namespace MediClubApp.Repositories.Base.CRUD;
public interface IDeleteAsync<TEntity>
{
    Task DeleteByIdAsync(Guid id);
}