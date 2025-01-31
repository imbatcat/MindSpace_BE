using MindSpace.Domain.Interfaces.Specifications;
using System.Linq.Expressions;

namespace MindSpace.Application.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        // =====================================
        // === Fields & Props
        // =====================================

        private readonly Expression<Func<T, bool>> _criteria;

        // =====================================
        // === Constructors
        // =====================================

        protected BaseSpecification() : this(null) { }

        /// <summary>
        /// Passing the query expression to build up the query
        /// </summary>
        /// <param name="criteria"></param>
        public BaseSpecification(
            Expression<Func<T, bool>>? criteria)
        {
            this._criteria = criteria;
        }

        // =====================================
        // === Methods
        // =====================================
        public Expression<Func<T, bool>>? Criteria => _criteria;

    }
}
