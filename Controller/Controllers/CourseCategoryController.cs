using ApplicationLayer.Services.CourseCategories;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers
{
    [ApiController]
    [Route("api/courseCategory")]
    public class CourseCategoryController : ControllerBase
    {
        private readonly CourseCategoryService _courseCategoryService;

        public CourseCategoryController(CourseCategoryService courseCategoryService)
        {
            _courseCategoryService = courseCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _courseCategoryService.GetAllCourseCategoriesAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var courseCategory = await _courseCategoryService.GetCourseCategoryByIdAsync(id);
            if (courseCategory == null)
            {
                return NotFound();
            }
            return Ok(courseCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CourseCategory courseCategory)
        {
            await _courseCategoryService.CreateCourseCategoryAsync(courseCategory);
            return CreatedAtAction(nameof(GetById), new { id = courseCategory.Id }, courseCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CourseCategory courseCategory)
        {
            if (id != courseCategory.Id)
            {
                return BadRequest();
            }
            await _courseCategoryService.UpdateCourseCategoryAsync(courseCategory);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _courseCategoryService.DeleteCourseCategoryAsync(id);
            return NoContent();
        }
    }

}
