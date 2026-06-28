using Matrimony.Application.Features.Auth.EmailVerification;
using Matrimony.Application.Features.Auth.ForgotPassword;
using Matrimony.Application.Features.Auth.Login;
using Matrimony.Application.Features.Auth.RefreshToken;
using Matrimony.Application.Features.Auth.Register;
using Matrimony.Application.Interfaces.Services;
using Matrimony.Shared.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;

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
            var response = await _authService.RegisterAsync(request);

            return Ok(
                ApiResponse<RegisterResponse>.SuccessResponse(
                    response,
                    "Registration successful."));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var response = await _authService.LoginAsync(request);

            return Ok(
                ApiResponse<LoginResponse>.SuccessResponse(
                    response,
                    "Login successful."));
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest request)
        {
            var response = await _authService.RefreshTokenAsync(request);

            return Ok(
                ApiResponse<RefreshTokenResponse>.SuccessResponse(
                    response,
                    "Token refreshed successfully."));
        }
        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole(Guid userId, string roleName)
        {
            await _authService.AssignRoleAsync(userId, roleName);

            return Ok(
                ApiResponse<object>.SuccessResponse(
                    null,
                    "Role assigned successfully."));
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            var response = await _authService.ForgotPasswordAsync(request);

            return Ok(
                ApiResponse<ForgotPasswordResponse>.SuccessResponse(
                    response,
                    response.Message));
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            await _authService.ResetPasswordAsync(request);

            return Ok(
                ApiResponse<object>.SuccessResponse(
                    null,
                    "Password has been reset successfully."));
        }
        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail(EmailVerificationRequest request)
        {
            await _authService.VerifyEmailAsync(request);

            return Ok(
                ApiResponse<object>.SuccessResponse(
                    null,
                    "Email verified successfully."));
        }
    }
}
