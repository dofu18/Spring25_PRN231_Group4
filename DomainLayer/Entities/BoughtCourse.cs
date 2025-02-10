using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class BoughtCourse : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public Guid? ChildId { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public Course Course { get; set; }
        public User Child { get; set; }
    }

}
