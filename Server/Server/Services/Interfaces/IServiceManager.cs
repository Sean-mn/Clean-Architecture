namespace Server.Services.Interfaces;

public interface IServiceManager
{
    public AuthService? AuthService { get; }
    public TokenService? TokenService { get; }
}