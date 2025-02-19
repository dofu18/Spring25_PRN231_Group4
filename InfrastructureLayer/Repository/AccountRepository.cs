using DomainLayer.Entities;
using InfrastructureLayer.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using DomainLayer.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DomainLayer.Enums.GeneralEnum;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.Repository
{
    public class AccountRepository : GenericRepository<User>, IAccountRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly TutoringKidDbContext _tutoringKidDbContext;

        public AccountRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, TutoringKidDbContext tutoringKidDbContext) : base(tutoringKidDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tutoringKidDbContext = tutoringKidDbContext;
        }
        public async Task<ResponseDto> CreateStaffAsync(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                return new ResponseDto { IsSucceed = true, Message = "User created successfully", Data = user };
            }
            return new ResponseDto { IsSucceed = false, Message = "User creation failed", Data = result.Errors };
        }
        public async Task<ResponseDto> CreateAdminAsync(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                return new ResponseDto { IsSucceed = true, Message = "User created successfully", Data = user };
            }
            return new ResponseDto { IsSucceed = false, Message = "User creation failed", Data = result.Errors };
        }
        public async Task<ResponseDto> DeleteUserAsync(string userId, UserStatusEnum status)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ResponseDto { IsSucceed = false, Message = "User not found" };
            }
            user.Status = UserStatusEnum.Disabled.ToString();

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return new ResponseDto { IsSucceed = true, Message = "User status changed successfully" };
            }
            else
            {
                return new ResponseDto { IsSucceed = false, Message = "Failed to change user status" };
            }
        }
        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<User> GetById(string userId)
        {
            if (Guid.TryParse(userId, out Guid userGuid))
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.Id == userGuid);
            }
            return null;
        }
    }
}
