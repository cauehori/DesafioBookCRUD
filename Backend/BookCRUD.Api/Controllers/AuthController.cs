using BookCRUD.Application.DTOs;
using BookCRUD.Application.Services;
using Microsoft.AspNetCore.Mvc;


namespace BookCRUD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO loginDto)
        {
            var token = _authService.Authenticate(loginDto);

            if (token == null)
            {
                return Unauthorized(new { message = "E-mail ou senha inválidos" });
            }

            return Ok(new { token }); 
        }
    }
}
