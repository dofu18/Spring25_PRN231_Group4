using ApplicationLayer.DTOs.Lesson;
using ApplicationLayer.Services.Lesson;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonsService _lessonService;

        public LessonsController(ILessonsService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllLessons()
        {
            var response = await _lessonService.GetAllLessonsAsync();
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetLessonsById(Guid lessonId)
        {
            var response = await _lessonService.GetLessonsByIdAsync(lessonId);
            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddLessons([FromBody] CreateLessonDto createLessonDto)
        {
            var response = await _lessonService.CreateLessonsAsync(createLessonDto);
            return Ok(response);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateLessons(Guid lessonId, [FromBody] UpdateLessonDto updateLessonDto)
        {
            var response = await _lessonService.UpdateLessonsAsync(lessonId, updateLessonDto);
            return Ok(response);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteLessons(Guid lessonId)
        {
            var response = await _lessonService.DeleteLessonsAsync(lessonId);
            return Ok(response);
        }
    }
}
