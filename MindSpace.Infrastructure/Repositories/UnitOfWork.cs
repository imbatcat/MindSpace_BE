namespace MindSpace.Infrastructure.Repositories;

using Domain.Entities;
using Domain.InterfaceRepos;
using MindSpace.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{



    public void Dispose()
    {

    }
    public IGenericRepository<T> Repository<T>() where T : BaseEntity => throw new NotImplementedException();
    public Task<int> CompleteAsync() => throw new NotImplementedException();
}
