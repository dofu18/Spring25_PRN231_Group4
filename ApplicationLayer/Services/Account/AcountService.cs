using ApplicationLayer.DTOs.Staff;
using ApplicationLayer.DTOs;
using AutoMapper;
using DomainLayer.Helper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DomainLayer.Enums.GeneralEnum;
using DomainLayer.Entities;
using ApplicationLayer.DTOs.Admin;
using ApplicationLayer.DTOs.Account;
using System.Security.Cryptography;
using System.Text;
using InfrastructureLayer.Repository.IRepository;

namespace ApplicationLayer.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IGenericRepository<User> _genericRepository;
        private readonly JwtHelper _jwtHelper;
        private readonly IMapper _mapper;

        public AccountService(
            IAccountRepository accountRepository,
            IGenericRepository<User> genericRepository,
            JwtHelper jwtHelper,
            IMapper mapper)
        {
            _accountRepository = accountRepository;
            _genericRepository = genericRepository;
            _jwtHelper = jwtHelper;
            _mapper = mapper;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            var hashedInput = HashPassword(password);
            return hashedInput == hashedPassword;
        }

        public async Task<ResponseDto> CreateStaffAsync(StaffDto staffDto, UserRoleEnum role)
        {
            var user = _mapper.Map<User>(staffDto);
            user.Id = Guid.NewGuid();
            user.RefreshToken = _jwtHelper.GenerateRefreshToken();
            user.RefreshTokenExpires = DateTime.UtcNow.AddDays(7);
            user.Status = UserStatusEnum.Active.ToString();
            user.Role = role.ToString();
            user.HashedPassword = HashPassword(staffDto.Password);

            return await _accountRepository.CreateStaffAsync(user, staffDto.Password);
        }

        public async Task<ResponseDto> CreateAdminAsync(AdminDto adminDto, UserRoleEnum role)
        {
            var user = _mapper.Map<User>(adminDto);
            user.Id = Guid.NewGuid();
            user.RefreshToken = _jwtHelper.GenerateRefreshToken();
            user.RefreshTokenExpires = DateTime.UtcNow.AddDays(7);
            user.Status = UserStatusEnum.Active.ToString();
            user.Role = role.ToString();
            user.HashedPassword = HashPassword(adminDto.Password);

            return await _accountRepository.CreateAdminAsync(user, adminDto.Password);
        }

        public async Task<ResponseDto> GetUserByIdAsync(string userId)
        {
            var user = await _accountRepository.GetById(userId);
            if (user != null)
            {
                var userDto = _mapper.Map<AccountDto>(user);
                return new ResponseDto { IsSucceed = true, Message = "User retrieved successfully", Data = userDto };
            }
            return new ResponseDto { IsSucceed = false, Message = "User not found" };
        }

        public async Task<ResponseDto> GetUserByEmailAsync(string email)
        {
            var user = await _accountRepository.GetByEmailAsync(email);
            if (user != null)
            {
                var userDto = _mapper.Map<AccountDto>(user);
                return new ResponseDto { IsSucceed = true, Message = "User retrieved successfully", Data = userDto };
            }
            return new ResponseDto { IsSucceed = false, Message = "User not found" };
        }

        public async Task<ResponseDto> GetAllUsersAsync()
        {
            var users = await _accountRepository.GetAllAsync();
            var userDtos = _mapper.Map<List<AccountDto>>(users);
            return new ResponseDto { IsSucceed = true, Message = "Users retrieved successfully", Data = userDtos };
        }

        public async Task<ResponseDto> UpdateUserAsync(string userId, AccountDto userDto)
        {
            var user = await _accountRepository.GetById(userId);
            if (user == null)
            {
                return new ResponseDto { IsSucceed = false, Message = "User not found" };
            }

            _mapper.Map(userDto, user);
            user.UserName = user.UserName.ToUpper();
            user.Email = user.Email.ToUpper();

            try
            {
                await _genericRepository.UpdateAsync(user);
                return new ResponseDto { IsSucceed = true, Message = "User updated successfully" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { IsSucceed = false, Message = $"Failed to update user: {ex.Message}" };
            }
        }

        public async Task<ResponseDto> DeleteUserAsync(string userId)
        {
            var user = await _accountRepository.GetById(userId);
            if (user == null)
            {
                return new ResponseDto { IsSucceed = false, Message = "User not found" };
            }

            try
            {
                user.Status = UserStatusEnum.Disabled.ToString();
                await _genericRepository.UpdateAsync(user);
                return new ResponseDto { IsSucceed = true, Message = "User status changed to disable successfully" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { IsSucceed = false, Message = $"Failed to disable user: {ex.Message}" };
            }
        }

        public async Task<ResponseDto> UpdateUserPasswordAsync(string email, UpdatePasswordDto updatePasswordDto)
        {
            var user = await _accountRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return new ResponseDto { IsSucceed = false, Message = "Email not found" };
            }

            if (!VerifyPassword(updatePasswordDto.CurrentPassword, user.HashedPassword))
            {
                return new ResponseDto { IsSucceed = false, Message = "Current password is incorrect" };
            }

            try
            {
                user.HashedPassword = HashPassword(updatePasswordDto.NewPassword);
                await _genericRepository.UpdateAsync(user);
                return new ResponseDto { IsSucceed = true, Message = "Password updated successfully" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { IsSucceed = false, Message = $"Failed to update password: {ex.Message}" };
            }
        }
    }
}