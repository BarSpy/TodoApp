using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Database.Abstracts
{
    public abstract class DbRepository<TEntity> : IDbRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly DatabaseContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public DbRepository(DatabaseContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> Entities => _dbSet.AsNoTracking();

        public virtual Task DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            return _context.SaveChangesAsync();
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            return _context.SaveChangesAsync();
        }
    }
}
