using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using WebApplication1.Data;
using WebApplication1.dtos;
using WebApplication1.models;
using DbContext = WebApplication1.Data.AppDbContext;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(
        DbContext context,
        IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserDTO model)
    {
        if (await _context.Users.AnyAsync(u => u.Email == model.Email))
        {
            return BadRequest(new { message = "Email já está em uso" });
        }

 
        var passwordHash = HashPassword(model.Password);


        var user = new User()
        {
            Email = model.Email,
            Password = passwordHash,

        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Registro realizado com sucesso." });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserDTO model)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email);
        
        if (user == null || !VerifyPassword(model.Password, user.Password))
        {
            return Unauthorized(new { message = "Email ou senha inválidos" });
        }

        // Gerar token JWT com apenas o ID do usuário
        var token = GenerateJwtToken(user.id);

        return Ok(new { token });
    }

    // Método para gerar token JWT com apenas o ID do usuário
    private string GenerateJwtToken(long userId)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    // Método para gerar hash de senha
    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    // Método para verificar senha
    private bool VerifyPassword(string password, string hash)
    {
        var passwordHash = HashPassword(password);
        return passwordHash == hash;
    }
}


