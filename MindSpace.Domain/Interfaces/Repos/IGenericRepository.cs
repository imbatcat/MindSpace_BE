namespace MindSpace.Domain.Interfaces.Repos;

using AutoMapper;
using Entities;
using MindSpace.Domain.Interfaces.Specifications;

public interface IGenericRepository<T> where T : BaseEntity
{
    // GET
    Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);
    Task<T?> GetBySpecAsync(ISpecification<T> spec);

    // GET support Projection with Automapper for better performance
    Task<TDto?> GetBySpecProjectedAsync<TDto>(ISpecification<T> spec, IConfigurationProvider mapperConfig);
    Task<IReadOnlyList<TDto>> GetAllWithSpecProjectedAsync<TDto>(ISpecification<T> spec, IConfigurationProvider mapperConfig);

    // COUNT
    Task<int> CountAsync(ISpecification<T> spec);

    // INSERT, DELETE, UPDATE
    public T Insert(T entity);
    public T Update(T entityToUpdate);
    public T Delete(T entityToDelete);
    public T Delete(object id);
}
