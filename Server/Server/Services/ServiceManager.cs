using Server.DBContexts;
using Server.Services.Interfaces;

namespace Server.Services;

public class ServiceManager : IServiceManager
{
    private readonly AuthDbContext _context;
    private AuthService? _authService;
    
    public ServiceManager(AuthDbContext context)
    {
        _context = context;
    }
    
    public AuthService AuthService => _authService ??= new AuthService(_context);
}