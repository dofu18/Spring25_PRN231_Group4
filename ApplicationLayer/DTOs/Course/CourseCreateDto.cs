using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Entities;

namespace ApplicationLayer.DTOs.Courses
{
    public class CourseCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float Price { get; set; }
        public int SlotQuantity { get; set; }
        public string CourseDetail { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public string Metadata { get; set; } = string.Empty;
        public float AvgRating { get; set; }
    }
}
