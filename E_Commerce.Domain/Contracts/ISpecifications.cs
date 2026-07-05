using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Domain.Entities;

namespace E_Commerce_Domain.Contracts
{
    public interface ISpecifications<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        List<Expression<Func<TEntity,object>>> IncludeExpressions { get; }
        Expression<Func<TEntity, bool>>? Criteria { get; }
    }
}
