using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTOs.Lesson
{
    public class LessonDto
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string? Content { get; set; }
        public int OrderIndex { get; set; }
    }
}
