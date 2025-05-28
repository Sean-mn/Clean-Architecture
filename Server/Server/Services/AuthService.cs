using Microsoft.EntityFrameworkCore;
using Server.DTOs;
using Server.Entities.Models;
using Server.Repositories.DBContexts;
using Server.Services.Interfaces;

namespace Server.Services;

public class AuthService : Service, IAuthService
{
    public AuthService(AuthDbContext context) : base(context)
    {
    }

    public async Task<User> RegisterAsync(RegisterRequestDto dto)
    {
        var exists = await _context.Users
            .AnyAsync(u => u.Username == dto.Username);

        if (exists)
            throw new Exception("이미 사용 중인 유저 이름");

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        
        var newUser = new User
        {
            Username = dto.Username,
            Password = hashedPassword
        };
        
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return newUser;
    }

    public async Task<User?> LoginAsync(LoginRequestDto dto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == dto.Username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            return null;
        
        return user;
    }
}