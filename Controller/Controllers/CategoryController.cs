using ApplicationLayer.DTOs.Category;
using ApplicationLayer.DTOs.TutorProfile;
using ApplicationLayer.Services.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllCategory()
        {
            var response = await _categoryService.GetAllCategoryAsync();
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoryById(Guid categoryId)
        {
            var response = await _categoryService.GetCategoryByIdAsync(categoryId);
            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddCategoryProfile([FromBody] CreateCategoryDto createCategoryDto)
        {
            var response = await _categoryService.CreateCategoryAsync(createCategoryDto);
            return Ok(response);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateCategory(Guid categoryId, [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            var response = await _categoryService.UpdateCategoryAsync(categoryId, updateCategoryDto);
            return Ok(response);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteCategory(Guid categoryId)
        {
            var response = await _categoryService.DeleteCategoryAsync(categoryId);
            return Ok(response);
        }
    }
}
