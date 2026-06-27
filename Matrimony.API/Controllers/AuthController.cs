using Matrimony.Application.Features.Auth.ForgotPassword;
using Matrimony.Application.Features.Auth.Login;
using Matrimony.Application.Features.Auth.RefreshToken;
using Matrimony.Application.Features.Auth.Register;
using Matrimony.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Matrimony.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);

            return Ok(result);
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest request)
        {
            var response = await _authService.RefreshTokenAsync(request);

            return Ok(response);
        }
        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole(Guid userId, string roleName)
        {
            await _authService.AssignRoleAsync(userId, roleName);

            return Ok(new
            {
                Message = "Role assigned successfully."
            });
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            var response = await _authService.ForgotPasswordAsync(request);

            return Ok(response);
        }
    }
}
