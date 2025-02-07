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
        public virtual Course Course { get; set; }
        public virtual Order Order { get; set; }
    }

}
