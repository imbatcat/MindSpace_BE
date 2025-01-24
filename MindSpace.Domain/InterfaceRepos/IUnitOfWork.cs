namespace MindSpace.Domain.InterfaceRepos;

using Entities;

/// <summary>
/// An interface for UnitOfWork
/// </summary>
public interface IUnitOfWork : IDisposable
{
    public IGenericRepository<T> Repository<T>() where T : BaseEntity;
    Task<int> CompleteAsync();
}
