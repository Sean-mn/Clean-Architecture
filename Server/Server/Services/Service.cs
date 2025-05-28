using Server.Repositories.DBContexts;

namespace Server.Services;

public abstract class Service
{
    protected AuthDbContext _context;
    
    protected Service(AuthDbContext context)
    {
        _context = context;
    }
}