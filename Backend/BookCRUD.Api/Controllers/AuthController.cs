using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookCRUD.Api.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BookCRUD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IConfiguration configuration) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO loginDto)
        {
            if (loginDto.Email == "admin@email.com" && loginDto.Password == "senha12345")
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var keyString = _configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt não configurado");
                var key = Encoding.ASCII.GetBytes(keyString);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new [] { new Claim(ClaimTypes.Email, loginDto.Email)}),
                    Expires = DateTime.UtcNow.AddDays(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return Ok(new { token = tokenHandler.WriteToken(token) });
            }

            return Unauthorized(new {message = "Email ou senha inválidos"} );
        }
    }
}
