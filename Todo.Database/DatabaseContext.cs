using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace Todo.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }

        public string DbPath { get; private set; }

        public DatabaseContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            Directory.CreateDirectory($"{path}{Path.DirectorySeparatorChar}TodoApp");
            DbPath = $@"{path}{Path.DirectorySeparatorChar}TodoApp{Path.DirectorySeparatorChar}todo.db";
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
    }
}
