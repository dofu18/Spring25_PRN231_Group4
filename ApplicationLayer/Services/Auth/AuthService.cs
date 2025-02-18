using ApplicationLayer.DTOs.Auth;
using ApplicationLayer.DTOs;
using DomainLayer.Helper;
using InfrastructureLayer.Repository;
using InfrastructureLayer.Repository.IRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using AutoMapper;
using DomainLayer.Entities;
using static DomainLayer.Enums.GeneralEnum;

namespace ApplicationLayer.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepo;
        private readonly ITutorProfileRepository _tutorProfileRepo;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly JwtHelper _jwtHelper;

        public AuthService(IAuthRepository authRepo, ITutorProfileRepository tutorProfileRepo, IConfiguration configuration, IMapper mapper, JwtHelper jwtHelper)
        {
            _authRepo = authRepo;
            _tutorProfileRepo = tutorProfileRepo;
            _configuration = configuration;
            _mapper = mapper;
            _jwtHelper = jwtHelper;
        }

        public async Task<ResponseDto> LoginAsync(LoginDto loginDto)
        {
            var response = new ResponseDto();

            var user = await _authRepo.GetByUserName(loginDto.Username);
            if (user == null)
            {
                response.Message = "Invalid credentials";
                return response;
            }

            var isPasswordValid = VerifyPassword(loginDto.Password, user.HashedPassword);

            if (!isPasswordValid)
            {
                response.Message = "Invalid credentials";
                return response;
            }

            var token = _jwtHelper.GenerateJwtToken(user);
            var refreshToken = _jwtHelper.GenerateRefreshToken();

            var tokenExpiration = DateTime.UtcNow.AddHours(1);
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(7);

            user.Token = token;
            user.TokenExpires = tokenExpiration;
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpires = refreshTokenExpiration;
            await _authRepo.UpdateAsync(user);

            response.IsSucceed = true;
            response.Message = "Login successful!";
            response.Data = new { Token = token, RefreshToken = refreshToken };

            return response;
        }

        public async Task<ResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var response = new ResponseDto();

            var existingUser = await _authRepo.GetByUserName(registerDto.Username);
            if (existingUser != null)
            {
                response.Message = "User already exists!";
                return response;
            }

            var account = _mapper.Map<User>(registerDto);

            account.Id = Guid.NewGuid();
            account.HashedPassword = HashPassword(registerDto.Password);
            account.Role = UserRoleEnum.Kid.ToString();
            account.Token = string.Empty;
            account.RefreshToken = string.Empty;

            await _authRepo.CreateAsync(account);

            var user = new User
            {
                Id = account.Id,
                UserName = account.UserName,
                Role = account.Role
            };

            response.IsSucceed = true;
            response.Message = "Registration successful!";
            response.Data = true;
            return response;
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string enteredPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);
        }
    }
}