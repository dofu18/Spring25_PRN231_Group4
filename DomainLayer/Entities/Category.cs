using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImgUrl { get; set; } = string.Empty;
        public bool Active { get; set; }

        // Navigation Properties
        public virtual User CreatedByUser { get; set; }
        public virtual ICollection<CourseCategory> CourseCategories { get; set; } = new List<CourseCategory>();
    }

}
