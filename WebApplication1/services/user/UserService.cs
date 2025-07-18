
using MongoDB.Driver;
using WebApplication1.dtos;
using WebApplication1.models;

namespace WebApplication1.services;

public class UserService
{
    private readonly IMongoCollection<User> _users;
        
    public UserService(IMongoDatabase database)
    {
        _users = database.GetCollection<User>("users");
    }

    public void DeleteUser(User user)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Id, user.Id);
    
        var update = Builders<User>.Update
            .Set(u => u.DeletedAt, DateTime.UtcNow)
            .Set(u => u.Status == UserStatus.INACTIVE,false); // supondo que exista esse campo
    
        _users.UpdateOne(filter, update);
    }

    public void UpdateUser(User user)
    {
        
        var filter = Builders<User>.Filter.Eq(u => u.Id, user.Id);
        
    }


    public void ForgotPassword(string email)
    {
        
    }
    

}