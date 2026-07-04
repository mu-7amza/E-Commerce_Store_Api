using E_Commerce_Domain.Contracts;
using E_Commerce_Domain.Entities;
using E_Commerce_Infrastructure.Date;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Infrastructure.Repository
{
    public class GenericRepository<TEntity, TKey>(StoreDbContext _context) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        
        public void Add(TEntity entity) => _context.Set<TEntity>().Add(entity);


        public void Delete(TEntity entity) => _context.Set<TEntity>().Remove(entity);
        

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct)
        {
           return await _context.Set<TEntity>().ToListAsync(ct);
        }

        public async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct)
        {
          return await _context.Set<TEntity>().FindAsync(id, ct);
        }

        public void Update(TEntity entity) => _context.Set<TEntity>().Update(entity);

        
    }
}
