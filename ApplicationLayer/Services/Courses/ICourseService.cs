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
        Task<DynamicResponse<CourseResponseModel>> GetAllCoursesAsync(GetAllCourseDto model);
        Task<GenericResp<CourseResponseModel>> UpdateCourseAsync(CourseCreateDto model, Guid id);
        Task<GenericResp<CourseResponseModel>> DeleteCourseAsync(Guid id, string status);
        Task<GenericResp<CourseResponseModel>> GetCourseByIdAsync( Guid id);
        Task<GenericResp<CourseResponseModel>> CreateCourseAsync(CourseCreateDto model);
        Task<ICollection<Course>> List(Guid? userId = null);
    }
}
