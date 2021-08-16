using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Database.Abstracts
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
