namespace MindSpace.Domain.Interfaces.Repos;

using Entities;

/// <summary>
/// An interface for UnitOfWork
/// </summary>
public interface IUnitOfWork : IDisposable
{

    public IGenericRepository<T> Repository<T>() where T : BaseEntity;
    /// <summary>
    ///     Save change async
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    Task<int> CompleteAsync();
}
