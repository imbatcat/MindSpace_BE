using System.Linq.Expressions;

namespace MindSpace.Domain.Interfaces.Specifications
{
    public interface ISpecification<T>
    {
        public Expression<Func<T, bool>>? Criteria { get; } // WHERE
        public Expression<Func<T, object>>? OrderBy { get; } // ORDER BY .. ASC
        public Expression<Func<T, object>>? OrderByDesc { get; } // ORDER BY .. DESC
        public int Skip { get; } // OFFSET 10 ROWS
        public int Take { get; } // FETCH NEXT 10 ROWS ONLY
        public bool IsPagingEnabled { get; }
        public bool IsDistinct { get; }

        List<Expression<Func<T, object>>> Includes { get; }// LEFT JOIN
        List<string> IncludeStrings { get; } // Orders.Include("OrderDetails.Person")
    }
}
