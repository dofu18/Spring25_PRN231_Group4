using ApplicationLayer.DTOs.Staff;
using ApplicationLayer.DTOs;
using AutoMapper;
using DomainLayer.Helper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DomainLayer.Enums.GeneralEnum;
using DomainLayer.Entities;
using InfrastructureLayer.Repository;
using InfrastructureLayer.Repository.IRepository;
using ApplicationLayer.DTOs.Admin;
using ApplicationLayer.DTOs.Account;

namespace ApplicationLayer.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IGenericRepository<User> _genericRepository;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtHelper _jwtHelper;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, JwtHelper jwtHelper, IMapper mapper, IPasswordHasher<DomainLayer.Entities.User> passwordHasher)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtHelper = jwtHelper;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }
        public async Task<ResponseDto> CreateStaffAsync(StaffDto staffDto, UserRoleEnum role)
        {
            var user = _mapper.Map<User>(staffDto);
            user.Id = Guid.NewGuid();
            user.RefreshToken = _jwtHelper.GenerateRefreshToken();
            user.RefreshTokenExpires = DateTime.UtcNow.AddDays(7);

            user.Status = UserStatusEnum.Active.ToString();
            var createUserResult = await _accountRepository.CreateStaffAsync(user, staffDto.Password);
            if (!createUserResult.IsSucceed)
            {
                return createUserResult;
            }

            var roleName = role.ToString();
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var createRoleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (!createRoleResult.Succeeded)
                {
                    return new ResponseDto { IsSucceed = false, Message = "Failed to create role: " + string.Join(", ", createRoleResult.Errors.Select(e => e.Description)) };
                }
            }

            var addToRoleResult = await _userManager.AddToRoleAsync(user, roleName);
            if (!addToRoleResult.Succeeded)
            {
                return new ResponseDto { IsSucceed = false, Message = "Failed to add staff to role: " + string.Join(", ", addToRoleResult.Errors.Select(e => e.Description)) };
            }

            return new ResponseDto { IsSucceed = true, Message = "Staff registered successfully" };
        }
        public async Task<ResponseDto> CreateAdminAsync(AdminDto adminDto, UserRoleEnum role)
        {
            var user = _mapper.Map<User>(adminDto);
            user.Id = Guid.NewGuid();
            user.RefreshToken = _jwtHelper.GenerateRefreshToken();
            user.RefreshTokenExpires = DateTime.UtcNow.AddDays(7);

            user.Status = UserStatusEnum.Active.ToString();
            var createUserResult = await _accountRepository.CreateAdminAsync(user, adminDto.Password);
            if (!createUserResult.IsSucceed)
            {
                return createUserResult;
            }

            var roleName = role.ToString();
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var createRoleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (!createRoleResult.Succeeded)
                {
                    return new ResponseDto { IsSucceed = false, Message = "Failed to create role: " + string.Join(", ", createRoleResult.Errors.Select(e => e.Description)) };
                }
            }

            var addToRoleResult = await _userManager.AddToRoleAsync(user, roleName);
            if (!addToRoleResult.Succeeded)
            {
                return new ResponseDto { IsSucceed = false, Message = "Failed to add admin to role: " + string.Join(", ", addToRoleResult.Errors.Select(e => e.Description)) };
            }

            return new ResponseDto { IsSucceed = true, Message = "Admin registered successfully" };
        }
        public async Task<ResponseDto> GetUserByIdAsync(string userId)
        {
            var userResponse = await _accountRepository.GetById(userId);
            if (userResponse != null)
            {
                var userDto = _mapper.Map<AccountDto>(userResponse);
                return new ResponseDto { IsSucceed = true, Message = "User retrieved successfully", Data = userDto };
            }
            return new ResponseDto { IsSucceed = false, Message = "User not found" };
        }
        public async Task<ResponseDto> GetUserByEmailAsync(string email)
        {
            var userResponse = await _accountRepository.GetByEmailAsync(email);
            if (userResponse != null)
            {
                var userDto = _mapper.Map<AccountDto>(userResponse);
                return new ResponseDto { IsSucceed = true, Message = "User retrieved successfully", Data = userDto };
            }
            return new ResponseDto { IsSucceed = false, Message = "User not found" };
        }
        public async Task<ResponseDto> UpdateUserAsync(string userId, AccountDto userDto)
        {
            var existingUser = await _accountRepository.GetById(userId);

            if (existingUser == null)
            {
                return new ResponseDto { IsSucceed = false, Message = "User not found" };
            }

            // Update the existingUser entity with data from userDto
            _mapper.Map(userDto, existingUser);
            existingUser.UserName = existingUser.UserName.ToUpper();
            existingUser.Email = existingUser.Email.ToUpper();
            try
            {
                await _genericRepository.UpdateAsync(existingUser);

                return new ResponseDto { IsSucceed = true, Message = "User updated successfully" };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseDto { IsSucceed = false, Message = "An error occurred while updating user" };
            }
        }
        public async Task<ResponseDto> DeleteUserAsync(string userId)
        {
            var user = await _accountRepository.GetById(userId);

            if (user == null)
            {
                return new ResponseDto { IsSucceed = false, Message = "User not found" };
            }

            user.Status = UserStatusEnum.Disabled.ToString();

            await _genericRepository.UpdateAsync(user);

            return new ResponseDto { IsSucceed = true, Message = "User status changed to disable successfully" };
        }
    }
}
