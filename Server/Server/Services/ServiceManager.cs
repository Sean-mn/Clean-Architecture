using Server.DBContexts;

namespace Server.Services;

public class ServiceManager
{
    private readonly AuthDbContext _context;
    private AuthService? _authService;
    
    public ServiceManager(AuthDbContext context)
    {
        _context = context;
    }
    
    public AuthService AuthService => _authService ??= new AuthService(_context);
}