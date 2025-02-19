using DomainLayer.Helper;
using ApplicationLayer.DTOs.Account;
using ApplicationLayer.DTOs.Admin;
using ApplicationLayer.DTOs.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DomainLayer.Enums.GeneralEnum;

namespace ApplicationLayer.Services.Account
{
    public interface IAccountService
    {
        Task<ResponseDto> CreateStaffAsync(StaffDto staffDto, UserRoleEnum role);
        Task<ResponseDto> CreateAdminAsync(AdminDto adminDto, UserRoleEnum role);
        Task<ResponseDto> GetUserByIdAsync(string userId);
        Task<ResponseDto> GetUserByEmailAsync(string email);
        Task<ResponseDto> GetAllUsersAsync();
        Task<ResponseDto> UpdateUserAsync(string userId, AccountDto userDto);
        Task<ResponseDto> DeleteUserAsync(string userId);
        Task<ResponseDto> UpdateUserPasswordAsync(string email, UpdatePasswordDto updatePasswordDto);
    }
}
