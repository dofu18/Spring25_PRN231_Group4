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
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Room { get; set; } = string.Empty;
        public Guid CourseId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public Guid? StudentId { get; set; }
        public int SlotQuantity { get; set; }

        // Navigation Properties
        public virtual Course Course { get; set; }
        public virtual User CreatedByUser { get; set; }
        public virtual User? Student { get; set; }
    }

}
