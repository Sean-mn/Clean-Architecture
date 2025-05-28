using Server.Entities.Models;

namespace Server.Services.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}