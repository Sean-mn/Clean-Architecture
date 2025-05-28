using Microsoft.EntityFrameworkCore;
using Server.DTOs;
using Server.Entities.Models;
using Server.Repositories.Interfaces;
using Server.Services.Interfaces;

namespace Server.Services;

public class AuthService : Service, IAuthService
{
    public AuthService(IUserRepository userRepository) : base(userRepository)
    {
    }

    public async Task<User> RegisterAsync(RegisterRequestDto dto)
    {
        var exists = await _userRepository.GetUserByUsernameAsync(dto.Username);

        if (exists != null)
            throw new Exception("이미 사용 중인 유저 이름");

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        
        var newUser = new User
        {
            Username = dto.Username,
            Password = hashedPassword
        };
        
        await _userRepository.AddUserAsync(newUser);

        return newUser;
    }

    public async Task<User?> LoginAsync(LoginRequestDto dto)
    {
        var user = await _userRepository.GetUserByUsernameAsync(dto.Username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            return null;
        
        return user;
    }
}