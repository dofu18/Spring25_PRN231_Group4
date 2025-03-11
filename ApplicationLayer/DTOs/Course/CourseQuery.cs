using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTOs.Courses
{
    public class CourseQuery : PaginationReq
    {
        public string? SearchKeyword { get; set; }
    }
}
