using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Todo.Database.Abstracts;

namespace Todo.Database.Extensions
{
    public static class DatabaseExtensions
    {
        public static void DetachLocal<TEntity>(this DatabaseContext context, TEntity entity) where TEntity : class, IEntity
        {
            var local = context.Set<TEntity>().Local.FirstOrDefault(e => e.Id.Equals(entity.Id));
            if (local != null)
            {
                context.Entry(local).State = EntityState.Detached;
            }
        }
    }
}
