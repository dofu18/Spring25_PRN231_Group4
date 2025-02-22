using DomainLayer.Entities;
using DomainLayer.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DomainLayer.Enums.GeneralEnum;

namespace InfrastructureLayer.Repository.IRepository
{
    public interface IAccountRepository : IGenericRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> GetById(string id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<ResponseDto> CreateStaffAsync(User user, string password);
        Task<ResponseDto> CreateAdminAsync(User user, string password);
        Task<ResponseDto> DeleteUserAsync(Guid userId, UserStatusEnum status);
    }
}
