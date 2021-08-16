using System;
using System.Collections.Generic;
using System.Text;
using Todo.Database;
using Todo.Database.Abstracts;

namespace Todo.Library.Abstracts
{
    public interface ITodoItemsService : IDbRepository<TodoItem>
    {
    }
}
