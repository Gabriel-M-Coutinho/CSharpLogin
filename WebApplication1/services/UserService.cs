using WebApplication1.Data;
using WebApplication1.dtos;
using WebApplication1.models;

namespace WebApplication1.services;

public class UserService
{
    private readonly UserDbContext _userDbContext;
    
    public UserService(UserDbContext context)
    {
        _userDbContext = context;
    }
    
    /*public async Task<User> CreateUserAsync(UserDTO dto)
    {
        var user = new User
        {
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user;
    }*/

}