using Microsoft.EntityFrameworkCore;
using Server.Entities.Models;
using Server.Repositories.DBContexts;
using Server.Repositories.Interfaces;

namespace Server.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AuthDbContext _context;

    public UserRepository(AuthDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _context.Users
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(int uid)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == uid);
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        try
        {
            return await _context.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username == username);
        }
        catch (Exception ex)
        {
            throw new Exception($"조회 중 오류 발생: {ex.Message}");
        }
    }

    public async Task<int> AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> DeleteUserAsync(User user)
    {
        _context.Users.Remove(user);
        return await _context.SaveChangesAsync();
    }
}