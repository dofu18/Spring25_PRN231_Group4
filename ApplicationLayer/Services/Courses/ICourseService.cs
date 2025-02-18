using ApplicationLayer.DTOs.Courses;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services.Courses
{
    public interface ICourseService
    {
        Task<ICollection<Course>> List(Guid? courseId = null);
        Task<Course?> GetById(Guid courseId);
        Task<IActionResult> Create(CourseDto course);
        Task<IActionResult> Update(CourseDto course);
        Task<IActionResult> Delete(Guid courseId);
    }

}
