using Server.Repositories.DBContexts;
using Server.Repositories.Interfaces;

namespace Server.Services;

public abstract class Service
{
    protected readonly IUserRepository _userRepository;

    protected Service(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
}