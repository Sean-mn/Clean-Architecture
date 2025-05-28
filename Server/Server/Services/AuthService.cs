using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.DBContexts;
using Server.DTOs;
using Server.Entities.Models;

namespace Server.Services;

public class AuthService : Service
{
    public AuthService(AuthDbContext context) : base(context)
    {
    }

    public async Task<User> RegisterAsync(RegisterRequestDto dto)
    {
        var exists = await _context.Users
            .AnyAsync(u => u.Username == dto.Username);

        if (exists)
        {
            throw new Exception("이미 사용 중인 유저 이름");
        }

        var newUser = new User
        {
            Username = dto.Username,
            Password = dto.Password
        };
        
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return newUser;
    }
}