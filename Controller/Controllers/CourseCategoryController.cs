using ApplicationLayer.DTOs.CourseCategory;
using ApplicationLayer.DTOs.TutorProfile;
using ApplicationLayer.Services.CourseCategories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseCategoryController : ControllerBase
    {
        private readonly ICourseCategoryService _courseCategoryService;

        public CourseCategoryController(ICourseCategoryService courseCategoryService)
        {
            _courseCategoryService = courseCategoryService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllCourseCategory()
        {
            var response = await _courseCategoryService.GetAllCourseCategoryAsync();
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetCourseCategoryById(Guid courseCategoryId)
        {
            var response = await _courseCategoryService.GetCourseCategoryByIdAsync(courseCategoryId);
            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddTutorProfile([FromBody] CreateCourseCategoryDto createCourseCategoryDto)
        {
            var response = await _courseCategoryService.CreateCourseCategoryAsync(createCourseCategoryDto);
            return Ok(response);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteTutorProfile(Guid courseCategoryId)
        {
            var response = await _courseCategoryService.DeleteCourseCategoryAsync(courseCategoryId);
            return Ok(response);
        }
    }
}
