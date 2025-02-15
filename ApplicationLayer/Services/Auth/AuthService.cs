using DomainLayer.Helper;
using InfrastructureLayer.Repository.IRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services.Auth
{
    public class AuthService
    {
        private readonly IAuthRepository _authRepo;
        private readonly ITutorProfileRepository _tutorProfileRepo;
        private readonly IConfiguration _configuration;
        private JwtHelper _jwtHelper;

        public AuthService(IAuthRepository authRepo, ITutorProfileRepository tutorProfileRepo, IConfiguration configuration)
        {
            _authRepo = authRepo;
            _tutorProfileRepo = tutorProfileRepo;
            _configuration = configuration;
            _jwtHelper = new JwtHelper(_configuration);
        }
    }
}
