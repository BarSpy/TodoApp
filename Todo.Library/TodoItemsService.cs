using System;
using System.Linq;
using System.Threading.Tasks;
using Todo.Database;
using Todo.Database.Abstracts;
using Todo.Library.Abstracts;

namespace Todo.Library
{
    public class TodoItemsService : DbRepository<TodoItem>, ITodoItemsService
    {
        public TodoItemsService(DatabaseContext context) : base(context)
        {
        }

        public IQueryable<TodoItem> GetAllForDate(DateTime target)
        {
            return Entities.Where(dbItem => dbItem.Target.Date == target.Date);
        }
    }
}
