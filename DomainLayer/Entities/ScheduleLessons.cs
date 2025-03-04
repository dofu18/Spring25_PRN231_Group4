using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class ScheduleLessons : BaseEntity
    {
        public Guid ScheduleId { get; set; }
        public Guid LessonsId { get; set; }
        public int SlotIndex { get; set; }

        //navigations properties
        public Schedule Schedule { get; set; }
        public Lessons Lessons { get; set; }
    }
}
