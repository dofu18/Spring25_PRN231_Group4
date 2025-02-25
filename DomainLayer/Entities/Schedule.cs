using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DomainLayer.Enums.GeneralEnum;

namespace DomainLayer.Entities
{
    public class Schedule : BaseEntity
    {
        public Guid CourseId { get; set; }
        public Guid? StudentId { get; set; } = Guid.Empty;
        public int SlotIndex { get; set; }
        public string DayOfWeek { get; set; } = string.Empty;
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public ScheduleStatusEnum Status { get; set; }
        // Navigation Properties
        public Course Course { get; set; }
        public User Student { get; set; }
    }

}
