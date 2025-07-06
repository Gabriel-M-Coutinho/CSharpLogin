using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MongoDB.Driver;
using WebApplication1.dtos;
using WebApplication1.models;
using WebApplication1.services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly JwtService _jwtService;
    private readonly PasswordService _passwordService;

    public AuthController(
        AuthService authService,
        JwtService jwtService,
        PasswordService passwordService)
    {
        _authService = authService;
        _jwtService = jwtService;
        _passwordService = passwordService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserDTO model)
    {
        try
        {
            if (await _authService.GetUserByEmailAsync(model.Email) != null)
            {
                return BadRequest(new { message = "Email já está em uso" });
            }

            var user = new User()
            {
                Email = model.Email,
                Password = _passwordService.HashPassword(model.Password)
            };

            await _authService.RegisterAsync(user);
            return Ok(new { message = "Registro realizado com sucesso." });
        }
        catch (MongoWriteException ex) when (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
        {
            return BadRequest(new { message = "Email já está em uso" });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserDTO model)
    {
        var user = await _authService.GetUserByEmailAsync(model.Email);
        
        if (user == null || !_passwordService.VerifyPassword(model.Password, user.Password))
        {
            return Unauthorized(new { message = "Email ou senha inválidos" });
        }

        var token = _jwtService.GenerateToken(user);
        return Ok(new { token });
    }
}