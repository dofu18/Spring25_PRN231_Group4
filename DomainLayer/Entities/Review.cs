using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class Review : BaseEntity
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public float Rating { get; set; }
        public bool Active { get; set; }

        // Navigation Properties
        public virtual Course Course { get; set; }
        public virtual User CreatedByUser { get; set; }
    }

}
