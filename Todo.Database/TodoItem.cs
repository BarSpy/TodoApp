using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Todo.Database.Abstracts;

namespace Todo.Database
{
    public class TodoItem : IEntity
    {
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        [NotMapped]
        private DateTime _target;

        public DateTime Target 
        {
            get
            {
                return DateTime.SpecifyKind(_target, DateTimeKind.Utc);
            }
            set
            {
                _target = new DateTime(value.Year, value.Month, value.Day);
            }
        }
    }
}
