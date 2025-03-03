using DomainLayer.Entities;
using InfrastructureLayer.Repository.IRepository;
using DomainLayer.Helper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DomainLayer.Enums.GeneralEnum;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.Repository
{
    public class AccountRepository : GenericRepository<User>, IAccountRepository
    {
        private readonly TutoringKidDbContext _tutoringKidDbContext;

        public AccountRepository(TutoringKidDbContext tutoringKidDbContext)
            : base(tutoringKidDbContext)
        {
            _tutoringKidDbContext = tutoringKidDbContext;
        }

        public async Task<ResponseDto> CreateStaffAsync(User user, string password)
        {
            try
            {
                await _tutoringKidDbContext.Users.AddAsync(user);
                await _tutoringKidDbContext.SaveChangesAsync();

                return new ResponseDto
                {
                    IsSucceed = true,
                    Message = "User created successfully",
                    Data = user
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto
                {
                    IsSucceed = false,
                    Message = $"User creation failed: {ex.Message}"
                };
            }
        }

        public async Task<ResponseDto> CreateAdminAsync(User user, string password)
        {
            try
            {
                await _tutoringKidDbContext.Users.AddAsync(user);
                await _tutoringKidDbContext.SaveChangesAsync();

                return new ResponseDto
                {
                    IsSucceed = true,
                    Message = "User created successfully",
                    Data = user
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto
                {
                    IsSucceed = false,
                    Message = $"User creation failed: {ex.Message}"
                };
            }
        }

        public async Task<ResponseDto> DeleteUserAsync(Guid userId, UserStatusEnum status)
        {
            try
            {
                var user = await _tutoringKidDbContext.Users
                    .FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                {
                    return new ResponseDto
                    {
                        IsSucceed = false,
                        Message = "User not found"
                    };
                }

                user.Status = UserStatusEnum.Disabled.ToString();
                _tutoringKidDbContext.Users.Update(user);
                await _tutoringKidDbContext.SaveChangesAsync();

                return new ResponseDto
                {
                    IsSucceed = true,
                    Message = "User status changed successfully"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto
                {
                    IsSucceed = false,
                    Message = $"Failed to change user status: {ex.Message}"
                };
            }
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _tutoringKidDbContext.Users
                .FirstOrDefaultAsync(u => u.Email.ToUpper() == email.ToUpper());
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _tutoringKidDbContext.Users.ToListAsync();
        }

        public async Task<User> GetById(string userId)
        {
            if (Guid.TryParse(userId, out Guid userGuid))
            {
                return await _tutoringKidDbContext.Users
                    .FirstOrDefaultAsync(u => u.Id == userGuid);
            }
            return null;
        }
    }
}