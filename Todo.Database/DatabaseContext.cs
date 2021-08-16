using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }

        public string DbPath { get; private set; }

        public DatabaseContext()
        {
            DbPath = $"todo.db";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
    }
}
