using ERPFlowItalia.Api.Data;
using ERPFlowItalia.Api.DTOs;
using ERPFlowItalia.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace ERPFlowItalia.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public AuthController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest(new AuthResponseDto
            {
                Success = false,
                Message = "Email and password are required."
            });
        }

        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        var user = await _dbContext.Users
            .Include(x => x.Role)
            .FirstOrDefaultAsync(x => x.Email.ToLower() == normalizedEmail);

        if (user == null || user.PasswordHash != HashPassword(request.Password))
        {
            return Unauthorized(new AuthResponseDto
            {
                Success = false,
                Message = "Invalid credentials."
            });
        }

        return Ok(new AuthResponseDto
        {
            Success = true,
            Message = "Access granted. Welcome back.",
            Token = BuildDemoToken(user),
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role?.Name ?? "User"
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.FullName) ||
            string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest(new AuthResponseDto
            {
                Success = false,
                Message = "Full name, email, and password are required."
            });
        }

        if (request.Password.Length < 8)
        {
            return BadRequest(new AuthResponseDto
            {
                Success = false,
                Message = "Password must contain at least 8 characters."
            });
        }

        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        var existingUser = await _dbContext.Users.AnyAsync(x => x.Email.ToLower() == normalizedEmail);
        if (existingUser)
        {
            return Conflict(new AuthResponseDto
            {
                Success = false,
                Message = "An account with this email already exists."
            });
        }

        var requestedRole = string.IsNullOrWhiteSpace(request.Role) ? "SalesTeam" : request.Role.Trim();
        var role = await _dbContext.Roles.FirstOrDefaultAsync(x => x.Name == requestedRole)
            ?? await _dbContext.Roles.FirstAsync(x => x.Name == "SalesTeam");

        var user = new User
        {
            FullName = request.FullName.Trim(),
            Email = normalizedEmail,
            PasswordHash = HashPassword(request.Password),
            RoleId = role.Id
        };

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        return Ok(new AuthResponseDto
        {
            Success = true,
            Message = "Account created successfully. You can now sign in.",
            Token = BuildDemoToken(user),
            FullName = user.FullName,
            Email = user.Email,
            Role = role.Name
        });
    }

    private static string HashPassword(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToHexString(bytes);
    }

    private static string BuildDemoToken(User user)
    {
        var raw = $"{user.Email}|{user.FullName}|{user.CreatedAt:O}";
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(raw));
    }
}
