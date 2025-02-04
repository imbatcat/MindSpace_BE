using MindSpace.Domain.Interfaces.Specifications;
using System.Linq.Expressions;

namespace MindSpace.Application.Specifications
{
    public class BaseSpecification<T> : ISpecificationEntity<T>
    {
        // =====================================
        // === Fields & Props
        // =====================================
        public Expression<Func<T, bool>>? Criteria { get; private set; }

        public Expression<Func<T, object>>? OrderBy { get; private set; }

        public Expression<Func<T, object>>? OrderByDesc { get; private set; }

        public int? Skip => throw new NotImplementedException();

        public int? Take => throw new NotImplementedException();

        public bool IsPagingEnabled { get; private set; }

        // =====================================
        // === Constructors
        // =====================================

        /// <summary>
        /// Passing the query expression to build up the query
        /// </summary>
        /// <param name="criteria"></param>
        public BaseSpecification(
            Expression<Func<T, bool>>? criteria)
        {
            Criteria = criteria;
        }

        // =====================================
        // === Methods
        // =====================================

        /// <summary>
        /// Add Expression Order By fields Ascending
        /// </summary>
        /// <param name="orderByExpression"></param>
        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        /// <summary>
        /// Add Expression Order By Fields Descending
        /// </summary>
        /// <param name="orderByDescExpression"></param>
        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDesc = orderByDescExpression;
        }
    }
}
