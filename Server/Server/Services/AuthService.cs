﻿using Server.DTOs;
using Server.Entities.Models;
using Server.Repositories.Interfaces;
using Server.Services.Interfaces;

namespace Server.Services;

public class AuthService : Service, IAuthService
{
    public AuthService(IUserRepository userRepository) : base(userRepository)
    {
    }

    public async Task<User?> RegisterAsync(RegisterRequestDto dto)
    {
        var exists = await _userRepository.GetUserByUsernameAsync(dto.Username);

        if (exists != null)
            return null;

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        
        var newUser = new User
        {
            Username = dto.Username,
            Password = hashedPassword
        };
        
        var result = await _userRepository.AddUserAsync(newUser);
        if (result == 0)
            return null;
        
        return newUser;
    }

    public async Task<User?> LoginAsync(LoginRequestDto dto)
    {
        var user = await _userRepository.GetUserByUsernameAsync(dto.Username);

        if (user == null)
            return null;
        
        bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);
        if (!isPasswordCorrect)
            return null;
        
        return user;
    }
}