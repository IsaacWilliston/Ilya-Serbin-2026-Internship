using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SeatsReservationDotNet.Entities;
using SeatsReservationDotNet.DTOs;

namespace SeatsReservationDotNet.Services;

public class TokenService(IConfiguration configuration) : ITokenService
{
    public AuthResponseDto GenerateToken(UserEntity user)
    {
        var jwtSettings = configuration.GetSection("Jwt");
        
        var secretKey = Environment.GetEnvironmentVariable("JWT_SIGNING_KEY") 
                        ?? jwtSettings["SigningKey"] 
                        ?? throw new InvalidOperationException("JWT Signing Key is not configured.");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            // Standardizes on uppercase strings like "ADMIN" / "CUSTOMER"
            new Claim(ClaimTypes.Role, user.Role.ToString()) 
        };

        var expiryMinutes = double.Parse(jwtSettings["ExpiryMinutes"] ?? "60");
        var expiresAt = DateTime.UtcNow.AddMinutes(expiryMinutes);

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: expiresAt,
            signingCredentials: creds
        );

        return new AuthResponseDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresAt = expiresAt
        };
    }
}