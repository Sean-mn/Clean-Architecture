using Server.Repositories.DBContexts;
using Server.Services.Interfaces;

namespace Server.Services;

public class ServiceManager : IServiceManager
{
    private readonly AuthDbContext _context;
    private readonly IConfiguration _config;
    
    private AuthService? _authService;
    private TokenService? _tokenService;
    
    public ServiceManager(AuthDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }
    
    public AuthService AuthService => _authService ??= new AuthService(_context);
    public TokenService TokenService => _tokenService ??= new TokenService(_config);
}