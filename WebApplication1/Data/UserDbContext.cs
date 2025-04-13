using Microsoft.AspNetCore.Connections.Features;
using Microsoft.EntityFrameworkCore;
using WebApplication1.models;

namespace WebApplication1.Data;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}

