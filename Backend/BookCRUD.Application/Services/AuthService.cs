using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookCRUD.Application.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookCRUD.Application.Services;

public class AuthService(IConfiguration configuration) : IAuthService
{
    private readonly IConfiguration _configuration = configuration;

    public string? Authenticate(LoginDTO loginDto)
    {
        if (loginDto.Email == "admin@email.com" && loginDto.Password == "senha12345")
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var keyString = _configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt não configurado");

            var key = Encoding.ASCII.GetBytes(keyString);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, loginDto.Email) }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token); 
        }

        return null; 
    }
}
