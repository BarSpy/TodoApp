using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Todo.Database;

namespace Todo.UI.Models
{
    public class TodoItemModel : INotifyPropertyChanged
    {
        public Guid Id { get; set; }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                NotifyPropertyChanged(nameof(Title));
            }
        }

        public DateTime CreatedAt { get; set; }

        private bool _completed;
        public bool Completed 
        { 
            get 
            { 
                return _completed; 
            } 
            set 
            { 
                _completed = value; 
                NotifyPropertyChanged(nameof(Completed)); 
            } 
        }

        public TodoItemModel(TodoItem source)
        {
            Id = source.Id;
            Title = source.Title;
            CreatedAt = source.CreatedAt.ToLocalTime().DateTime;
            Completed = source.Completed;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
