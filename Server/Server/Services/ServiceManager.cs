﻿using Server.Repositories.Interfaces;
using Server.Services.Interfaces;

namespace Server.Services;

public class ServiceManager : IServiceManager
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _config;
    
    private AuthService? _authService;
    private TokenService? _tokenService;
    
    public ServiceManager(IUserRepository userRepository, IConfiguration config)
    {
        _userRepository = userRepository;
        _config = config;
    }
    
    public IAuthService AuthService => _authService ??= new AuthService(_userRepository);
    public ITokenService TokenService => _tokenService ??= new TokenService(_config);
}