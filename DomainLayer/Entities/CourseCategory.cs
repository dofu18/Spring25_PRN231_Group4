using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class CourseCategory : BaseEntity
    {
        public Guid CourseId { get; set; }
        public Guid CategoryId { get; set; }

        // Navigation Properties
        public virtual Course Course { get; set; }
        public virtual Category Category { get; set; }
    }

}
