using System.ComponentModel.DataAnnotations;

namespace WebApplication1.dtos;

public class UserDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}