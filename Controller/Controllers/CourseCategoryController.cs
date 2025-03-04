using ApplicationLayer.DTOs.CourseCategory;
using ApplicationLayer.Services.CourseCategories;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers
{
    [ApiController]
    [Route("api/courseCategory")]
    public class CourseCategoryController : ControllerBase
    {
        private readonly ICourseCategoryService _courseCategoryService;

        public CourseCategoryController(ICourseCategoryService courseCategoryService)
        {
            _courseCategoryService = courseCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(GetAllCategoryCourseRequestModel model)
        {
            return Ok(await _courseCategoryService.GetAllCourseCategoriesAsync(model));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await _courseCategoryService.GetCourseCategoryByIdAsync(id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CourseCategoryCreateDto model)
        {
            try
            {
                var result = await _courseCategoryService.CreateCourseCategoryAsync(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = "Admin, Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CourseCategoryCreateDto model)
        {
            try
            {
                var result = await _courseCategoryService.UpdateCourseCategoryAsync(model, id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = "Admin, Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, bool status)
        {
            try
            {
                var result = await _courseCategoryService.DeleteCourseCategoryAsync(id, status);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
