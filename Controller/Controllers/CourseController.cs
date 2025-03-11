using ApplicationLayer.DTOs.Course;
using ApplicationLayer.Services.Courses;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers
{
    [ApiController]
    [Route("api/course")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(CourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost("Search")]
        public async Task<IActionResult> GetAllCourses(GetAllCourseDto model)
        {
            try
            {
                var result = await _courseService.GetAllCoursesAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(Guid id)
        {
            try
            {
                var result = await _courseService.GetCourseByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateCourse(CourseCreateDto model)
        {
            try
            {
                var result = await _courseService.CreateCourseAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet]
        public async Task<IActionResult> UpdateCourse([FromBody]CourseCreateDto model, Guid id)
        {
            try
            {
                var result = await _courseService.UpdateCourseAsync(model, id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = "Admin, Manager")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCourse(Guid id, string status)
        {
            try
            {
                var result = await _courseService.DeleteCourseAsync(id, status);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
