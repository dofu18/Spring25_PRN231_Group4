using ApplicationLayer.DTOs.Account;
using ApplicationLayer.DTOs.Admin;
using ApplicationLayer.DTOs.Staff;
using ApplicationLayer.Services.Account;
using DomainLayer.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static DomainLayer.Enums.GeneralEnum;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly JwtHelper _jwtHelper;

        public AccountController(IAccountService accountService, JwtHelper jwtHelper)
        {
            _accountService = accountService;
            _jwtHelper = jwtHelper;
        }

        [HttpPost("CreateStaff")]
        public async Task<IActionResult> CreateStaff([FromBody] StaffDto staffDto)
        {
            var response = await _accountService.CreateStaffAsync(staffDto, UserRoleEnum.Staff);
            if (response.IsSucceed)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPost("CreateAdmin")]
        public async Task<IActionResult> CreateAdmin([FromBody] AdminDto adminDto)
        {
            var response = await _accountService.CreateAdminAsync(adminDto, UserRoleEnum.Admin);
            if (response.IsSucceed)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var response = await _accountService.GetUserByIdAsync(id.ToString());
            if (response.IsSucceed)
            {
                return Ok(response);
            }

            return NotFound(response);
        }

        [HttpGet("GetUserByEmail/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var response = await _accountService.GetUserByEmailAsync(email);
            if (response.IsSucceed)
            {
                return Ok(response);
            }

            return NotFound(response);
        }

        [HttpPut("UpdateUser/{userId}")]
        public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] AccountDto user)
        {
            var response = await _accountService.UpdateUserAsync(userId.ToString(), user);
            if (response.IsSucceed)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpDelete("DeleteUser/{userId}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            var response = await _accountService.DeleteUserAsync(userId.ToString());
            if (response.IsSucceed)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPut("UpdatePassword/{email}")]
        public async Task<IActionResult> UpdatePassword(string email, [FromBody] UpdatePasswordDto updatePasswordDto)
        {
            var result = await _accountService.UpdateUserPasswordAsync(email, updatePasswordDto);

            if (result.IsSucceed)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
