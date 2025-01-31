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
        public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec)
        {
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria); // x => x.Brand == brand
            }

            return query;
        }
    }
}
