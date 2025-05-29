using Server.DTOs;
using Server.Entities.Models;

namespace Server.Services.Interfaces;

public interface IAuthService
{
    Task<User?> RegisterAsync(RegisterRequestDto dto);
    Task<User?> LoginAsync(LoginRequestDto dto);
}