using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Database;
using Todo.Database.Abstracts;

namespace Todo.Library.Abstracts
{
    public interface ITodoItemsService : IDbRepository<TodoItem>
    {
        IQueryable<TodoItem> GetAllForDate(DateTime target);
    }
}
