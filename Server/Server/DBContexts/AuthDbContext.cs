using Microsoft.EntityFrameworkCore;
using Server.Entities.Models;

namespace Server.DBContexts;

public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
}