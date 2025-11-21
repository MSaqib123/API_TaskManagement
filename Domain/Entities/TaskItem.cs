using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public Priority Priority { get; set; } = Priority.Medium;
        public string? Category { get; set; }
        public string? Status { get; set; } = "Open";
        public string? Recurrence { get; set; }
        public DateTime? DueDate { get; set; }
        public int OrderIndex { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}


public enum Priority
{
    Low = 0,
    Medium = 1,
    High = 2,
    Urgent = 3
}
