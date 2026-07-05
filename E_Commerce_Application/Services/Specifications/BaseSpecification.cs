using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Domain.Contracts;
using E_Commerce_Domain.Entities;

namespace E_Commerce_Application.Services.Specifications
{
    public abstract class BaseSpecification<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        protected BaseSpecification(Expression<Func<TEntity,bool>> criteria = null)
        {
            Criteria = criteria;
        }
        public void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            IncludeExpressions.Add(includeExpression);
        }
    }
}
