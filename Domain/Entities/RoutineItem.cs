using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
 
    public class RoutineItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public string? StartTime { get; set; } // HH:mm
        public string? EndTime { get; set; } // HH:mm
        public DateTime CreatedAt { get; set; }
    }
}
