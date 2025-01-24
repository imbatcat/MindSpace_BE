namespace MindSpace.Infrastructure.Repositories;

using Domain.Entities;
using Domain.InterfaceRepos;

public class UnitOfWork : IUnitOfWork {
    public void Dispose()
    {
        throw new NotImplementedException();
    }
    public IGenericRepository<T> Repository<T>() where T : BaseEntity => throw new NotImplementedException();
    public Task<int> CompleteAsync() => throw new NotImplementedException();
}
