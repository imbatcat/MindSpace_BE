namespace MindSpace.Infrastructure.Repositories;

using Domain.Entities;
using Domain.InterfaceRepos;
using Microsoft.Extensions.Logging;
using Persistence;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity {
    // ===========================
    // === Fields & Props
    // ===========================

    private readonly ApplicationDbContext _context;

    private readonly ILogger _logger;

    // ===========================
    // === Constructors
    // ===========================

    public GenericRepository(ApplicationDbContext context, ILogger<GenericRepository<T>> logger)
    {
        _context = context;
        _logger = logger;
    }

    // ===========================
    // === Methods
    // ===========================

    /// <summary>
    /// Insert a new record of a entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public int? Insert(T entity)
    {
        try{
            if (entity == null) throw new ArgumentNullException($"[{nameof(T)}] Insert {entity} failed.");
            _context.Set<T>().Add(entity);

            return entity.Id;
        }
        catch (ArgumentNullException ex){
            _logger.LogError(ex.Message, ex);
        }
        return null;
    }

    public int? Update(T entityToUpdate) => throw new NotImplementedException();
    public int? Delete(T entityToDelete) => throw new NotImplementedException();
    public int? Delete(object id) => throw new NotImplementedException();
}
