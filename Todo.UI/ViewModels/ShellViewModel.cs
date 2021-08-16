using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Database;
using Todo.Library.Abstracts;

namespace Todo.UI.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private string _title = "Walk a dog...";

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private DateTime _target = DateTime.UtcNow;

        public DateTime Target
        {
            get { return _target; }
            set { _target = value; }
        }

        private readonly ITodoItemsService _todoItemsService;

        public ShellViewModel(ITodoItemsService todoItemsService)
        {
            _todoItemsService = todoItemsService;
            var items = _todoItemsService.Entities.ToList();
        }

        public async Task AddTodo()
        {
            var todo = new TodoItem(title: Title, target: Target);
            await _todoItemsService.InsertAsync(todo);
        }
    }
}
