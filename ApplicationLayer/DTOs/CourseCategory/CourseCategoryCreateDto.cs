using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTOs.CourseCategory
{
    public class CourseCategoryCreateDto
    {
        public Guid CourseId { get; set; }
        public Guid CategoryId { get; set; }
        public string Status { get; set; }
    }
}
