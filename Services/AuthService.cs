using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SeatsReservationDotNet.Data;
using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.Entities;
using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.Services;

public class AuthService(
    AppDbContext context,
    IPasswordHasher<UserEntity> passwordHasher,
    ITokenService tokenService) : IAuthService
{
    public async Task<GetUserDto> RegisterAsync(RegisterDto dto)
    {
        var emailLower = dto.Email.ToLowerInvariant();

        // Enforce uniqueness check at the service level
        var exists = await context.Users.AnyAsync(u => u.Email == emailLower);
        if (exists)
        {
            throw new InvalidOperationException("An account with this email already exists.");
        }

        var user = new UserEntity
        {
            Email = emailLower,
            Role = Role.CUSTOMER
        };

        user.PasswordHash = passwordHasher.HashPassword(user, dto.Password);

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return new GetUserDto
        {
            Id = user.Id,
            Email = user.Email,
            Role = user.Role
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var emailLower = dto.Email.ToLowerInvariant();

        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == emailLower);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        var verificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if (verificationResult == PasswordVerificationResult.Failed)
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        return tokenService.GenerateToken(user);
    }
}