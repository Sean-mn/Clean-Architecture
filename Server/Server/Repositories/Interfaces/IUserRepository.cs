using Server.Entities.Models;

namespace Server.Repositories.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(int uid);
    Task<int> AddUserAsync(User user);
    Task<int> UpdateUserAsync(User user);
    Task<int> DeleteUserAsync(User user);
}