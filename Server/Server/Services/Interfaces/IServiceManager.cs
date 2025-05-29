namespace Server.Services.Interfaces;

public interface IServiceManager
{
    public IAuthService? AuthService { get; }
    public ITokenService? TokenService { get; }
}