using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication1.models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("email")]
    public string Email { get; set; }

    [BsonElement("password")]
    public string Password { get; set; }
    
    public string Gender { get; set; }
    
    public int age { get; set; }
    
    public UserStatus Status { get; set; }
    
    public ERole Role { get; set; }
    
    public DateTime? DeletedAt { get; set; }
    
    public DateTime CreatedAt { get; set; }



    public User()
    {
        CreatedAt = DateTime.Now;
        Status = UserStatus.ACTIVE;
    }

    public User(string email, string password , ERole role)
    {
        Email = email;
        Password = password;
        Role = role;
        CreatedAt = DateTime.Now;
        Status = UserStatus.ACTIVE;
    }
}