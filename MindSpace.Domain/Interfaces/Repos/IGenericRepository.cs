namespace MindSpace.Domain.Interfaces.Repos;

using Entities;
using MindSpace.Domain.Interfaces.Specifications;

public interface IGenericRepository<T> where T : BaseEntity
{
    // GET
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecificationEntity<T> spec);
    Task<T?> GetEntityWithSpec(ISpecificationEntity<T> spec);

    // COUNT
    Task<int> CountAsync();

    // INSERT, DELETE, UPDATE
    public T Insert(T entity);
    public T Update(T entityToUpdate);
    public T Delete(T entityToDelete);
    public T Delete(object id);
}
