using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Database.Abstracts
{
    public interface IDbRepository<T> where T : class, IEntity
    {
        IQueryable<T> Entities { get; }
        Task<T> InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
