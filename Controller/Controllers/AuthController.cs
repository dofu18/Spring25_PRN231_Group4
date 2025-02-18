using ApplicationLayer.DTOs.Auth;
using ApplicationLayer.Services.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto userRegisterDto)
        {
            var result = await _authService.RegisterAsync(userRegisterDto);
            if (!result.IsSucceed)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto userLoginDto)
        {
            var result = await _authService.LoginAsync(userLoginDto);
            if (!result.IsSucceed)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
