using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Database;
using Todo.Library.Abstracts;
using Todo.UI.Models;

namespace Todo.UI.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        public BindableCollection<TodoItemModel> TodoItems { get; set; }

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
            set
            {
                _target = value;
                LoadTodoItems();
            }
        }

        private readonly ITodoItemsService _todoItemsService;

        public ShellViewModel(ITodoItemsService todoItemsService)
        {
            _todoItemsService = todoItemsService;
            LoadTodoItems();
        }

        public bool CanAddTodo(string title)
        {
            return string.IsNullOrWhiteSpace(Title) == false;
        }

        public async Task AddTodo(string title)
        {
            var todo = new TodoItem(title: Title, target: Target);
            await _todoItemsService.InsertAsync(todo);
            var todoModel = new TodoItemModel(todo);
            todoModel.PropertyChanged += PropChanged;
            TodoItems.Add(todoModel);
        }

        public void Delete(object item)
        {
            if (item is TodoItemModel todoItem)
            {
                var dbItem = _todoItemsService.GetSingleAsync(todoItem.Id).Result;
                _todoItemsService.DeleteAsync(dbItem).Wait();
                TodoItems.Remove(todoItem);
            }
        }

        private void LoadTodoItems()
        {
            var items = _todoItemsService
                .GetAllForDate(Target)
                .ToList()
                .Select(dbItem => new TodoItemModel(dbItem))
                .Select(item =>
                {
                    item.PropertyChanged += PropChanged;
                    return item;
                });

            TodoItems = new BindableCollection<TodoItemModel>(items);
            NotifyOfPropertyChange(() => TodoItems);
        }

        private void PropChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is TodoItemModel item)
            {
                var dbItem = _todoItemsService.GetSingleAsync(item.Id).Result;
                dbItem.Completed = item.Completed;
                dbItem.Title = item.Title;
                _todoItemsService.UpdateAsync(dbItem).Wait();
            }
        }
    }
}
