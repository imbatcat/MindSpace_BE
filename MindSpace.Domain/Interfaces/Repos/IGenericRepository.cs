namespace MindSpace.Domain.Interfaces.Repos;

using Entities;

public interface IGenericRepository<T> where T : BaseEntity
{
    // public Task<IEnumerable<T>> GetAllAsync();
    // public Task<T> GetAsync();

    public T Insert(T entity);
    public T Update(T entityToUpdate);
    public T Delete(T entityToDelete);
    public T Delete(object id);
}
