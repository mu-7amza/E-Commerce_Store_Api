using E_Commerce_Domain.Contracts;
using E_Commerce_Domain.Entities;
using E_Commerce_Infrastructure.Date;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Infrastructure.Repository
{
    public class UnitOfWork(StoreDbContext _context) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var typeName = typeof(TEntity).Name;
            if (_repositories.TryGetValue(typeName, out object value))
                return (IGenericRepository<TEntity,TKey>) value;
            else
            {
                var repo = new GenericRepository<TEntity, TKey>(_context);
                _repositories[typeName] = repo;
                return repo;
            }
            
                
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct = default) => await _context.SaveChangesAsync(ct);
    }
}
