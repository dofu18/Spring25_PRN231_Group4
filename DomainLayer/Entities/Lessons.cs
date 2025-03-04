using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.RenderTree;

namespace DomainLayer.Entities
{
    public class Lessons : BaseEntity
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string? Content { get; set; }
        public int OrderIndex { get; set; }

        //navigations
        public Course Course { get; set; }
    }
}
