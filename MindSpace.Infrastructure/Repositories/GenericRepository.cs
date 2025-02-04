namespace MindSpace.Infrastructure.Repositories;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Specifications;
using MindSpace.Domain.Interfaces.Repos;
using MindSpace.Domain.Interfaces.Specifications;
using Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    // ===========================
    // === Fields & Props
    // ===========================

    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger _logger;

    // ===========================
    // === Constructors
    // ===========================

    public GenericRepository(ApplicationDbContext context, ILogger<GenericRepository<T>> logger)
    {
        _dbContext = context;
        _logger = logger;
    }

    // ===========================
    // === INSERT, UPDATE, DELETE
    // ===========================

    /// <summary>
    /// Insert a new record of a entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public T Insert(T entity)
    {
        try
        {
            if (entity == null) throw new ArgumentNullException($"[{nameof(T)}] Insert {entity} failed.");
            var addedEntity = _dbContext.Set<T>().Add(entity).Entity;

            return addedEntity;
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex.Message, ex);
        }
        return null;
    }

    /// <summary>
    /// Update Entity
    /// </summary>
    /// <param name="entityToUpdate"></param>
    /// <returns></returns>
    public T Update(T entityToUpdate)
    {
        try
        {
            if (entityToUpdate == null) throw new ArgumentNullException($"[{nameof(T)}] Update {entityToUpdate} failed.");

            var updatedEntity = _dbContext.Set<T>().Update(entityToUpdate).Entity;
            _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
            return updatedEntity;
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex.Message, ex);
        }
        return null;
    }

    /// <summary>
    /// Delete an entity
    /// </summary>
    /// <param name="entityToDelete"></param>
    /// <returns></returns>
    public T Delete(T entityToDelete)
    {
        try
        {
            if (entityToDelete == null) throw new ArgumentNullException($"[{nameof(T)}] Delete {entityToDelete} not exists.");

            if (_dbContext.Set<T>().Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbContext.Set<T>().Attach(entityToDelete);
            }
            var deletedEntity = _dbContext.Set<T>().Remove(entityToDelete).Entity;
            return deletedEntity;
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex.Message, ex);
        }

        return null;
    }

    /// <summary>
    /// Delete an object base on id 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public T Delete(object id)
    {
        try
        {
            if (id == null) throw new ArgumentNullException($"[{nameof(T)}] Delete {id} failed.");

            T? entityToDelete = _dbContext.Set<T>().Find(id);
            return Delete(entityToDelete);
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex.Message, ex);
        }

        return null;
    }

    // ===================================
    // === GET queries with Specification
    // ===================================


    /// <summary>
    /// Apply the spec into the query
    /// </summary>
    /// <param name="spec"></param>
    /// <returns></returns>
    private IQueryable<T> ApplySpecification(ISpecificationEntity<T> spec)
    {
        // convert collection into iqueryable, enable deferred execution
        IQueryable<T> query = _dbContext.Set<T>().AsQueryable();

        return SpecificationEvaluator<T>.GetQuery(query, spec);
    }

    /// <summary>
    /// Get the list based on spec
    /// </summary>
    /// <param name="spec"></param>
    /// <returns></returns>
    public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecificationEntity<T> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    /// <summary>
    /// Get the individual item based on spec
    /// </summary>
    /// <param name="spec"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<T?> GetEntityWithSpec(ISpecificationEntity<T> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    /// <summary>
    /// Get all records of the entity
    /// </summary>
    /// <returns></returns>
    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    /// <summary>
    /// Get entity by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbContext.Set<T>().FirstOrDefaultAsync();
    }

    /// <summary>
    /// Count number of records with filter
    /// </summary>
    /// <param name="spec"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<int> CountAsync(ISpecificationEntity<T> spec)
    {
        var query = _dbContext.Set<T>().AsQueryable();
        return await SpecificationEvaluator<T>.GetCountQuery(query, spec).CountAsync();
    }
}
