using ApplicationLayer.DTOs.Courses;
using ApplicationLayer.Services.Courses;
using DomainLayer.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers
{
    [Route(Constants.Http.API_VERSION + "/Course")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private ILogger<CourseController> _logger;

        public CourseController(ILogger<CourseController> logger, ICourseService courseService)
        {
            _logger = logger;
            _courseService = courseService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CourseDto courseDto)
        {
            _logger.LogInformation("Create course request received");

            return await _courseService.Create(courseDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation("Search course by id request received");

            var result = await _courseService.GetById(id);
            return Ok(result);
        }
    }
}
