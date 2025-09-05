using ExpenseTrackerApp.Data;
using ExpenseTrackerApp.Models;
using ExpenseTrackerApp.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


// Replace this line:
// PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)

// With the following code, using the available BCrypt methods and signatures:

namespace ExpenseTrackerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;


        public UsersController(AppDbContext context,IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto request)
        {
            if (await _context.Users.AnyAsync(u => u.UserName == request.Username))
                return BadRequest("User already exists");

            var user = new User
            {
                UserName = request.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDto request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials");

            var token = GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var keyString = jwtSettings["Key"];
            if (string.IsNullOrEmpty(keyString))
            {
                throw new InvalidOperationException("JWT Key is missing in configuration.");
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));


            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var ExpireMinutesStr = jwtSettings["ExpireMinutes"];
            if (string.IsNullOrEmpty(ExpireMinutesStr))
            {
                throw new InvalidOperationException("JWT ExpireMinutes is missing in configuration.");
            }

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(ExpireMinutesStr)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
