using MongoDB.Driver;
using WebApplication1.models;

namespace WebApplication1.services;

public class AuthService
{
    private readonly IMongoCollection<User> _users;
        
    public AuthService(IMongoDatabase database)
    {
        _users = database.GetCollection<User>("users");
        CreateEmailIndex();
    }

    private void CreateEmailIndex()
    {
        var indexKeys = Builders<User>.IndexKeys.Ascending(u => u.Email);
        var indexOptions = new CreateIndexOptions { Unique = true };
        _users.Indexes.CreateOne(new CreateIndexModel<User>(indexKeys, indexOptions));
    }

    public async Task<User> RegisterAsync(User user)
    {
        await _users.InsertOneAsync(user);
        return user;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
    }
}
