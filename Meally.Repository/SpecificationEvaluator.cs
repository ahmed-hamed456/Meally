using Meally.core.Entities.Identity;
using Meally.core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meally.Repository
{
    public static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuary(IQueryable<TEntity> inputQuary, ISpecification<TEntity> spec)
        {
            var query = inputQuary;

            if (spec.Criteria is not null)
                query = query.Where(spec.Criteria);

            if (spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);
            else if(spec.OrderByDes is not null)
                    query = query.OrderByDescending(spec.OrderByDes);


            query = spec.Includes.Aggregate(query, (currentQuary, includeExpression) => currentQuary.Include(includeExpression));

            return query;
        }
    }
}
