using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Domain.Contracts;
using E_Commerce_Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Infrastructure.Repository
{
    // Dynamic query builder for specifications.
    public class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> specification) where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery;
            if (specification.IncludeExpressions.Count > 0)
            {

                query = specification.IncludeExpressions.Aggregate(query, (current, includeExpression) => current.Include(includeExpression));
            }
            if(specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }
            return query;
        }
    }
}
