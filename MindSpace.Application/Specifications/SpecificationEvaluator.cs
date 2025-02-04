using MindSpace.Domain.Entities;
using MindSpace.Domain.Interfaces.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Specifications
{
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        /// <summary>
        /// Attach the Expression into IQueryable
        /// </summary>
        /// <param name="query"></param>
        /// <param name="spec"></param>
        /// <returns></returns>
        public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecificationEntity<T> spec)
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

            return query;
        }
    }
}
