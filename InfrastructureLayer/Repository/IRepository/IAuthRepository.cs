using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Repository.IRepository
{
    public interface IAuthRepository : IGenericRepository<User>
    {
        Task<User?> GetByUserName(string Username);
        Task<User> GetByEmail(string email);
    }
}
