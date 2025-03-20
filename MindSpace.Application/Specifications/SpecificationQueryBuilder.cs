using Microsoft.EntityFrameworkCore;
using MindSpace.Application.Interfaces.Specifications;

namespace MindSpace.Application.Specifications
{
    public class SpecificationQueryBuilder<T> where T : class
    {
        /// <summary>
        /// Attach the Expression into IQueryable
        /// </summary>
        /// <param name="query"></param>
        /// <param name="spec"></param>
        /// <returns></returns>
        public static IQueryable<T> BuildQuery(IQueryable<T> query, ISpecification<T> spec)
        {
            // WHERE x.Brand = "Hitachi"
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            // ORDER BY x.Brand ASC
            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            // ORDER BY x.Brand DESC
            if (spec.OrderByDesc != null)
            {
                query = query.OrderByDescending(spec.OrderByDesc);
            }

            // DISTINCT
            if (spec.IsDistinct)
            {
                query = query.Distinct();
            }

            // Paging with SKIP, TAKE
            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            // Include
            if (spec.Includes.Count > 0)
            {
                query = spec.Includes.Aggregate(query, (curr, include) => curr.Include(include));
            }

            // Include and ThenInclude
            if (spec.IncludeStrings.Count > 0)
            {
                query = spec.IncludeStrings.Aggregate(query, (curr, include) => curr.Include(include));
            }

            // TOP
            if (spec.Top != null)
            {
                query = query.Take(spec.Top.Value);
            }

            return query;
        }

        /// <summary>
        /// Get Count Entities Query
        /// </summary>
        /// <param name="query"></param>
        /// <param name="spec"></param>
        /// <returns></returns>
        public static IQueryable<T> BuildCountQuery(IQueryable<T> query, ISpecification<T> spec)
        {
            // WHERE x.Brand = "Hitachi"
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            // DISTINCT
            // - Distinct can affect performance, recommend not using
            if (spec.IsDistinct != null)
            {
                query = query.Distinct();
            }

            return query;
        }

        /// <summary>
        /// Build the specific group by query
        /// </summary>
        /// <param name="query"></param>
        /// <param name="spec"></param>
        /// <returns></returns>
        public static IQueryable<T> BuildGroupByQuery(IQueryable<T> query, ISpecification<T> spec)
        {
            // WHERE x.Brand = "Hitachi"
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            // GROUP BY, HAVING, SELECT
            if (spec.GroupBy != null)
            {
                var groupedByQuery = query.GroupBy(spec.GroupBy);

                // HAVING 
                if (spec.Having != null)
                {
                    groupedByQuery = groupedByQuery.Where(spec.Having);
                }

                // SELECT a, avg(a)
                // - must implement due to the behavior of Select statement
                var resultQuery = groupedByQuery.Select(spec.Select!);

                // ORDER BY x.Brand ASC.
                if (spec.OrderBy != null)
                {
                    resultQuery = resultQuery.OrderBy(spec.OrderBy);
                }

                // ORDER BY x.Brand DESC
                if (spec.OrderByDesc != null)
                {
                    resultQuery = resultQuery.OrderByDescending(spec.OrderByDesc);
                }

                // Paging with SKIP, TAKE
                if (spec.IsPagingEnabled)
                {
                    resultQuery = resultQuery.Skip(spec.Skip).Take(spec.Take);
                }

                return resultQuery;
            }

            return query;
        }
    }
}