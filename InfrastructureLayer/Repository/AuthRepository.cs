using DomainLayer.Entities;
using InfrastructureLayer.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Repository
{
    public class AuthRepository : GenericRepository<User>, IAuthRepository
    {
        private readonly TutoringKidDbContext _appDbContext;
        public AuthRepository(TutoringKidDbContext appDbContext) : base(appDbContext)
        {
            {
                _appDbContext = appDbContext;
            }
        }
        public async Task<User> GetByUserName(string Username)
        {
            return await _appDbContext.Users.SingleOrDefaultAsync(u => u.UserName == Username);
        }
        public async Task<User> GetByEmail(string email)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
