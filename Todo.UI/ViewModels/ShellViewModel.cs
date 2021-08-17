using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Todo.Database;
using Todo.Library.Abstracts;
using Todo.UI.Models;

namespace Todo.UI.ViewModels
{
    public class ShellViewModel : Screen
    {
        public TodoItemModel SelectedTodo { get; set; }

        private BindableCollection<TodoItemModel> _todoItems;
        public BindableCollection<TodoItemModel> TodoItems
        {
            get { return _todoItems; }
            set
            {
                _todoItems = value;
                NotifyOfPropertyChange(() => TodoItems);
            }
        }

        private string _title = "Walk a dog...";
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyOfPropertyChange(() => Title);
            }
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
            try
            {
                var todo = new TodoItem(title: Title, target: Target);
                await _todoItemsService.InsertAsync(todo);
                var todoModel = new TodoItemModel(todo);
                todoModel.PropertyChanged += PropChanged;
                TodoItems.Add(todoModel);
                Title = string.Empty;
            }
            catch
            {
                MessageBox.Show("An error occured while saving a todo. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Delete(object item)
        {
            try
            {
                if (item is TodoItemModel todoItem)
                {
                    var dbItem = _todoItemsService.GetSingleAsync(todoItem.Id).Result;
                    _todoItemsService.DeleteAsync(dbItem).Wait();
                    TodoItems.Remove(todoItem);
                }
            }
            catch
            {
                MessageBox.Show("An error occured while deleting a todo. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SwitchCompletionLevel(object item)
        {
            try
            {
                if (SelectedTodo != null)
                {
                    var dbItem = _todoItemsService.GetSingleAsync(SelectedTodo.Id).Result;
                    dbItem.Completed = !dbItem.Completed;
                    _todoItemsService.UpdateAsync(dbItem).Wait();
                    SelectedTodo.Completed = dbItem.Completed;
                }
            }
            catch
            {
                MessageBox.Show("An error occured while switching a todo status. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadTodoItems()
        {
            IEnumerable<TodoItemModel> items = _todoItemsService
                .GetAllForDate(Target)
                .ToList()
                .Select(dbItem => new TodoItemModel(dbItem))
                .Select(item =>
                {
                    item.PropertyChanged += PropChanged;
                    return item;
                });

            TodoItems = new BindableCollection<TodoItemModel>(items);
        }

        private void PropChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                if (sender is TodoItemModel item)
                {
                    var dbItem = _todoItemsService.GetSingleAsync(item.Id).Result;
                    dbItem.Title = item.Title;
                    _todoItemsService.UpdateAsync(dbItem).Wait();
                }
            }
            catch
            {
                MessageBox.Show("An error occured while updating a todo title. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
