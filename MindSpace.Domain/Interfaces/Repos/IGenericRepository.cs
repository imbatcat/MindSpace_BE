namespace MindSpace.Domain.Interfaces.Repos;

using Entities;
using MindSpace.Domain.Interfaces.Specifications;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> spec);
    Task<T?> GetEntityWithSpec(ISpecification<T> spec);
    public T Insert(T entity);
    public T Update(T entityToUpdate);
    public T Delete(T entityToDelete);
    public T Delete(object id);
}
