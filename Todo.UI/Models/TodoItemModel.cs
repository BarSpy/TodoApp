using System;
using System.Collections.Generic;
using System.Text;
using Todo.Database;

namespace Todo.UI.Models
{
    public class TodoItemModel
    {
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }

        public TodoItemModel(TodoItem source)
        {
            Title = source.Title;
            CreatedAt = source.CreatedAt.ToLocalTime().DateTime;
        }
    }
}
