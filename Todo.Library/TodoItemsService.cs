using System;
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
    }
}
