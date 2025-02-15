using ApplicationLayer.DTOs.Auth;
using ApplicationLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services.Auth
{
    public interface IAuthService
    {
        Task<ResponseDto> RegisterAsync(RegisterDto registerDTO);
        Task<ResponseDto> LoginAsync(LoginDto loginDTO);
    }
}
