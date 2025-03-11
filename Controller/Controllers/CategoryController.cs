using ApplicationLayer.DTOs.Category;
using ApplicationLayer.Services.Categories;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(GetAllCategoryRequestModel model)
        {
            try
            {
                var result = await _categoryService.GetAllCategoriesAsync(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await _categoryService.GetCategoryByIdAsync(id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDto model)
        {
            try
            {
                var result = await _categoryService.CreateCategoryAsync(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = "Admin, Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] CategoryCreateDto model, Guid id)
        {
            try
            {
                var result = await _categoryService.UpdateCategoryAsync(model, id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = "Admin, Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, string status)
        {
            try
            {
                var result = await _categoryService.DeleteCategoryAsync(id, status);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
