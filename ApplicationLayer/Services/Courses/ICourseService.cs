using Application.RespType;
using ApplicationLayer.DTOs.Course;
using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services.Courses
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task UpdateCourseAsync(Course course);
        Task DeleteCourseAsync(Guid id);
        Task CreateCourseAsync(Course course);
        Task<Course> GetCourseByIdAsync(Guid id);
    }
}
