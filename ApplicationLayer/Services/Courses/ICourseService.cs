using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationLayer.DTOs.Courses;
using Microsoft.AspNetCore.Mvc;
using static DomainLayer.Enums.GeneralEnum;

namespace ApplicationLayer.Services.Courses
{
    public interface ICourseService
    {
        Task<IActionResult> HandleGetByIdAsync(Guid courseId);
        Task<IActionResult> HandleUpdateAsync(Guid courseId, CourseUpdateDto dto);
        Task<IActionResult> GetCourseActive(CourseQuery query);
        Task<IActionResult> HandleCreateCourse(CourseCreateDto dto, Guid userId);
        Task<IActionResult> GetAllCourseAsync(CourseQuery query, CourseStatusEnum? status);
        Task<IActionResult> HandleStatusAsync(Guid courseId, CourseStatusEnum status);
        Task<IActionResult> HandleDeleteAsync(Guid courseId);
    }
}
