using Microsoft.EntityFrameworkCore;
using Server.Entities.Models;

namespace Server.Repositories.DBContexts;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
}