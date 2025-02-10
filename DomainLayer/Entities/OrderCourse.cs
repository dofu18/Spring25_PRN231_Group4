using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class OrderCourse : BaseEntity
    {
        public Guid CourseId { get; set; }
        public Guid OrderId { get; set; }
        public int Discount { get; set; }
        public float Price { get; set; }

        // Navigation Properties
        public Course Course { get; set; }
        public Order Order { get; set; }
    }

}
