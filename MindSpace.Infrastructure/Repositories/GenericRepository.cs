namespace MindSpace.Infrastructure.Repositories;

using Application.Specifications;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Specifications;
using Persistence;

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

    public T? Insert(T entity)
    {
        var addedEntity = _dbContext.Set<T>().Add(entity).Entity;
        return addedEntity;
    }

    public T? Update(T entityToUpdate)
    {
        var updatedEntity = _dbContext.Set<T>().Update(entityToUpdate).Entity;
        _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
        return updatedEntity;
    }

    public T? Delete(T entityToDelete)
    {
        // Attach if the entry not tracked in in-memory
        if (_dbContext.Set<T>().Entry(entityToDelete).State == EntityState.Detached)
        {
            _dbContext.Set<T>().Attach(entityToDelete);
        }
        var deletedEntity = _dbContext.Set<T>().Remove(entityToDelete).Entity;
        return deletedEntity;
    }

    public T? Delete(object id)
    {
        var entityToDelete = _dbContext.Set<T>().Find(id);
        return entityToDelete == null ? null : Delete(entityToDelete);
    }

    // ========================================
    // === GET queries with Specification
    // === Using with Include and .ThenInclude
    // ========================================

    public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
    {
        IQueryable<T> query = _dbContext.Set<T>().AsQueryable();
        return await SpecificationQueryBuilder<T>.BuildQuery(query, spec).ToListAsync();
    }

    public async Task<T?> GetBySpecAsync(ISpecification<T> spec)
    {
        IQueryable<T> query = _dbContext.Set<T>().AsQueryable();
        return await SpecificationQueryBuilder<T>.BuildQuery(query, spec).FirstOrDefaultAsync();
    }

    public async Task<int> CountAsync(ISpecification<T> spec)
    {
        IQueryable<T> query = _dbContext.Set<T>().AsQueryable();
        return await SpecificationQueryBuilder<T>.BuildCountQuery(query, spec).CountAsync();
    }

    // ===========================================
    // === GET queries Projection with AutoMapper
    // === Using with Profile and Dto
    // ===========================================
    public async Task<TDto?> GetBySpecProjectedAsync<TDto>(ISpecification<T> spec, IConfigurationProvider mapperConfig)
    {
        IQueryable<T> query = _dbContext.Set<T>().AsQueryable();
        return await SpecificationQueryBuilder<T>.BuildQuery(query, spec)
            .ProjectTo<TDto>(mapperConfig)
            .FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<TDto>> GetAllWithSpecProjectedAsync<TDto>(ISpecification<T> spec, IConfigurationProvider mapperConfig)
    {
        IQueryable<T> query = _dbContext.Set<T>().AsQueryable();
        return await SpecificationQueryBuilder<T>.BuildQuery(query, spec)
            .ProjectTo<TDto>(mapperConfig)
            .ToListAsync();
    }

    // ===========================================
    // === GET queries with GROUP BY 
    // ===========================================

    public async Task<IReadOnlyList<T>> GetAllGroupByAsync(ISpecification<T> spec)
    {
        IQueryable<T> query = _dbContext.Set<T>().AsQueryable();
        return await SpecificationQueryBuilder<T>.BuildGroupByQuery(query, spec).ToListAsync();
    }
}
