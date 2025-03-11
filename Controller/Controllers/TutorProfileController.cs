using ApplicationLayer.DTOs.TutorProfile;
using ApplicationLayer.Services.TutorProfiles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorProfileController : ControllerBase
    {
        private readonly ITutorProfileService _tutorProfileService;

        public TutorProfileController(ITutorProfileService tutorProfileService)
        {
            _tutorProfileService = tutorProfileService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllTutorProfile()
        {
            var response = await _tutorProfileService.GetAllTutorProfileAsync();
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetTutorProfileById(Guid tutorId)
        {
            var response = await _tutorProfileService.GetTutorProfileByIdAsync(tutorId);
            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddTutorProfile([FromBody] CreateTutorProfileDto createTutorProfileDto)
        {
            var response = await _tutorProfileService.CreateTutorProfileAsync(createTutorProfileDto);
            return Ok(response);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateTutorProfile(Guid tutorId, [FromBody] UpdateTutorProfileDto updateTutorProfileDto)
        {
            var response = await _tutorProfileService.UpdateTutorProfileAsync(tutorId, updateTutorProfileDto);
            return Ok(response);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteTutorProfile(Guid tutorId)
        {
            var response = await _tutorProfileService.DeleteTutorProfileAsync(tutorId);
            return Ok(response);
        }
    }
}
