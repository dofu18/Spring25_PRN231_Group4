using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class Course : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float Price { get; set; }
        public int Discount { get; set; }
        public string Status { get; set; } = string.Empty;
        public string CourseDetail { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public string Metadata { get; set; } = string.Empty;
        public float AvgRating { get; set; }

        // Navigation Properties
        public virtual User Tutor { get; set; }
        public virtual ICollection<CourseCategory> CourseCategories { get; set; } = new List<CourseCategory>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }

}
