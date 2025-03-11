using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ApplicationLayer.DTOs.Courses;
using ApplicationLayer.Services.Courses;
using DomainLayer.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static DomainLayer.Enums.GeneralEnum;

namespace Controller.Controllers
{
    [Route(Constants.Http.API_VERSION + "/Course")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _service;
        private ILogger<CourseController> _logger;

        public CourseController(ILogger<CourseController> logger, ICourseService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("{courseId}")]
        public async Task<IActionResult> GetCourseById([FromRoute] Guid courseId)
        {
            _logger.LogInformation("Get course by id log");

            return await _service.HandleGetByIdAsync(courseId);
        }

        [HttpPut("{courseId}")]
        public async Task<IActionResult> Update(Guid courseId, [FromBody] CourseUpdateDto dto)
        {
            _logger.LogInformation("Update course log");

            return await _service.HandleUpdateAsync(courseId, dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetCoursePublish([FromQuery] CourseQuery query)
        {
            _logger.LogInformation("Get course publish log");

            return await _service.GetCourseActive(query);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CourseCreateDto dto)
        {
            _logger.LogInformation("Create course log");

            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdString == null)
                return Unauthorized();

            var userId = Guid.Parse(userIdString);

            return await _service.HandleCreateCourse(dto, userId);
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllCourse([FromQuery] CourseQuery query, CourseStatusEnum? status)
        {
            _logger.LogInformation("Get all course log");

            return await _service.GetAllCourseAsync(query, status);
        }

        [HttpPatch("{courseId}")]
        public async Task<IActionResult> HandleStatus(Guid courseId, [FromQuery] CourseStatusEnum status)
        {
            _logger.LogInformation("Handle status course log");

            return await _service.HandleStatusAsync(courseId, status);
        }

        [HttpDelete("{courseId}")]
        public async Task<IActionResult> DeleteCourse(Guid courseId)
        {
            _logger.LogInformation("Delete course log");

            return await _service.HandleDeleteAsync(courseId);
        }
    }
}
