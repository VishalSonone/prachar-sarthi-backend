using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PracharSaarathi.Api.Data;
using PracharSaarathi.Api.Models;

namespace PracharSaarathi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // For demo purposes, we accept any admin with phone 1234567890 and password Password123
            // Or use the database seeded user
            var admin = await _context.Admins.FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber);
            
            if (admin == null || request.Password != "Password123") // Simple password check for demo
            {
                return Unauthorized(new { message = "Invalid phone number or password" });
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecret = _configuration["Jwt:Secret"] ?? "a_very_long_and_secure_secret_key_for_demo_123456";
            var key = Encoding.ASCII.GetBytes(jwtSecret);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, admin.Name),
                    new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()),
                    new Claim(ClaimTypes.MobilePhone, admin.PhoneNumber)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                Token = tokenString,
                AdminName = admin.Name,
                PhoneNumber = admin.PhoneNumber
            });
        }
    }
}
