using MindSpace.Domain.Entities;

namespace MindSpace.Application.Interfaces.Repos;
/// <summary>
/// An interface for UnitOfWork
/// </summary>
public interface IUnitOfWork : IDisposable
{
    public IGenericRepository<T> Repository<T>() where T : BaseEntity;

    Task<int> CompleteAsync();
}
