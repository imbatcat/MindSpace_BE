using System.Linq.Expressions;

namespace MindSpace.Domain.Interfaces.Specifications
{
    // Support WHERE query
    public interface ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; }
    }
}
