using MindSpace.Domain.Interfaces.Specifications;
using System.Linq.Expressions;

namespace MindSpace.Application.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        // =====================================
        // === Fields & Props
        // =====================================
        public Expression<Func<T, bool>>? Criteria { get; private set; }

        public Expression<Func<T, object>>? OrderBy { get; private set; }

        public Expression<Func<T, object>>? OrderByDesc { get; private set; }

        public int Skip { get; private set; }

        public int Take { get; private set; }

        public bool IsPagingEnabled { get; private set; }

        public bool IsDistinct { get; private set; }

        public List<Expression<Func<T, object>>> Includes { get; } = new();

        public List<string> IncludeStrings { get; } = new();

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

        /// <summary>
        /// Apply Paging
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        protected void AddPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }

        /// <summary>
        /// Apply Distinct to the query
        /// </summary>
        protected void AddDistinct()
        {
            IsDistinct = true;
        }

        /// <summary>
        /// Apply an extra criteria
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        protected IQueryable<T> AddCriteria(IQueryable<T> query)
        {
            if (Criteria != null)
            {
                query = query.Where(Criteria);
            }
            return query;
        }

        /// <summary>
        /// Support Include 1 level only
        /// </summary>
        /// <param name="includeExpression"></param>
        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        /// <summary>
        /// Support Nested n level
        /// </summary>
        /// <param name="includeName"></param>
        protected void AddInclude(string includeName)
        {
            IncludeStrings.Add(includeName);
        }
    }
}