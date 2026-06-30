using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using dashboard_api.Interfaces;
using dashboard_api.Models;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace dashboard_api.Services;

public class TokenService : ITokenService
{
    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, user.Username),
            new("UserId", user.Id.ToString()),
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("a-string-secret-at-least-256-bits-long"));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "SmartHomeApi",
            audience: "SmartHomeClient",
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: creds
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}