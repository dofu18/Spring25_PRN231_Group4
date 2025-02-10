using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class Schedule : BaseEntity
    {
        public string DayOfWeek { get; set; } = string.Empty;
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Room { get; set; } = string.Empty;
        public Guid CourseId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public Guid? StudentId { get; set; }
        public int SlotQuantity { get; set; }

        // Navigation Properties
        public Course Course { get; set; }
        public User CreatedByUser { get; set; }
        public User? Student { get; set; }
    }

}
