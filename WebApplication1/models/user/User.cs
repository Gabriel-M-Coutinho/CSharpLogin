using System.ComponentModel.DataAnnotations;

namespace WebApplication1.models;

public class User
{
    [Key]
    public long id { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public User()
    {
    }

    public User(string email, string password)
    {
        Email = email;
        Password = password;
    }
    
}