using Matrimony.Application.Features.Auth.EmailVerification;
using Matrimony.Application.Features.Auth.ForgotPassword;
using Matrimony.Application.Features.Auth.Login;
using Matrimony.Application.Features.Auth.RefreshToken;
using Matrimony.Application.Features.Auth.Register;
using Matrimony.Application.Interfaces.Repositories;
using Matrimony.Application.Interfaces.Services;
using Matrimony.Domain.Entities;
using Matrimony.Infrastructure.Authentication;
using Matrimony.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly ApplicationDbContext _context;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IEmailService _emailService;

        public AuthService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    RoleManager<IdentityRole<Guid>> roleManager,
    IUserRepository userRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IJwtService jwtService,
    IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtService = jwtService;
            _emailService = emailService;
        }
        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            // Check email
            if (await _userRepository.EmailExistsAsync(request.Email))
                throw new Exception("Email already exists.");

            // Check phone
            if (await _userRepository.PhoneExistsAsync(request.PhoneNumber))
                throw new Exception("Phone number already exists.");

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email,
                PhoneNumber = request.PhoneNumber,
                IsActive = true,
                IsVerified = false,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                throw new Exception(
                    string.Join(", ", result.Errors.Select(x => x.Description)));
            }

            // Assign default role (we'll create it later)
            // await _userManager.AddToRoleAsync(user, "FreeMember");

            return new RegisterResponse
            {
                UserId = user.Id,
                Message = "Registration successful."
            };
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            // Find user by email
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                throw new Exception("Invalid email or password.");

            // Verify password
            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                request.Password,
                lockoutOnFailure: true);

            if (!result.Succeeded)
                throw new Exception("Invalid email or password.");

            var refreshToken = GenerateRefreshToken();

            await _refreshTokenRepository.AddAsync(new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                ExpiryDate = DateTime.UtcNow.AddDays(30),
                IsRevoked = false
            }); ;

            return new LoginResponse
            {
                UserId = user.Id,
                FullName = $"{user.FirstName} {user.LastName}",
                Email = user.Email!,
                AccessToken = _jwtService.GenerateToken(user),
                RefreshToken = refreshToken,
                Message = "Login successful."
            };
        }

        private static string GenerateRefreshToken()
        {
            var randomBytes = RandomNumberGenerator.GetBytes(64);

            return Convert.ToBase64String(randomBytes);
        }
        public async Task<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequest request)
        {
            // Find refresh token
            var existingToken = await _refreshTokenRepository
                .GetByTokenAsync(request.RefreshToken);

            if (existingToken == null)
                throw new Exception("Invalid refresh token.");

            if (existingToken.IsRevoked)
                throw new Exception("Refresh token has been revoked.");

            if (existingToken.ExpiryDate <= DateTime.UtcNow)
                throw new Exception("Refresh token has expired.");

            var user = existingToken.User;

            // Revoke all active refresh tokens for this user
            await _refreshTokenRepository.RevokeAllAsync(user.Id);

            // Generate new tokens
            var newAccessToken = _jwtService.GenerateToken(user);

            var newRefreshToken = GenerateRefreshToken();

            await _refreshTokenRepository.AddAsync(new RefreshToken
            {
                UserId = user.Id,
                Token = newRefreshToken,
                ExpiryDate = DateTime.UtcNow.AddDays(30),
                IsRevoked = false
            });

            return new RefreshTokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(15)
            };
        }
        public async Task AssignRoleAsync(Guid userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                throw new Exception("User not found.");

            // Create the role if it doesn't exist
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var createRoleResult = await _roleManager.CreateAsync(
                    new IdentityRole<Guid>(roleName));

                if (!createRoleResult.Succeeded)
                {
                    throw new Exception(
                        string.Join(", ", createRoleResult.Errors.Select(e => e.Description)));
                }
            }

            // Avoid assigning the same role twice
            if (await _userManager.IsInRoleAsync(user, roleName))
                return;

            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (!result.Succeeded)
            {
                throw new Exception(
                    string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
     

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            // Don't reveal whether the email exists
            if (user == null)
            {
                return new ForgotPasswordResponse
                {
                    Message = "If an account with this email exists, a password reset link has been sent."
                };
            }

            // Generate password reset token
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Encode token for URL
            var encodedToken = Uri.EscapeDataString(token);

            // Temporary frontend URL
            var resetLink =
                $"https://localhost:3000/reset-password?email={user.Email}&token={encodedToken}";

            var body = $@"
        <h2>Matrimony Password Reset</h2>

        <p>Hello {user.FirstName},</p>

        <p>Please click the link below to reset your password.</p>

        <p>
            <a href='{resetLink}'>Reset Password</a>
        </p>

        <p>This link will expire automatically.</p>

        <p>If you didn't request this, please ignore this email.</p>";

            await _emailService.SendEmailAsync(
                user.Email!,
                "Reset Password",
                body);

            return new ForgotPasswordResponse
            {
                Message = "If an account with this email exists, a password reset link has been sent."
            };
        }

        public async Task ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                throw new Exception("Invalid request.");

            // Decode the token because it was URL encoded before sending in email
            var decodedToken = Uri.UnescapeDataString(request.Token);

            var result = await _userManager.ResetPasswordAsync(
                user,
                decodedToken,
                request.NewPassword);

            if (!result.Succeeded)
            {
                throw new Exception(
                    string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
        public async Task VerifyEmailAsync(EmailVerificationRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                throw new Exception("Invalid verification request.");

            // Decode the token because it was URL encoded in the email
            var decodedToken = Uri.UnescapeDataString(request.Token);

            var result = await _userManager.ConfirmEmailAsync(
                user,
                decodedToken);

            if (!result.Succeeded)
            {
                throw new Exception(
                    string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
    }
}
