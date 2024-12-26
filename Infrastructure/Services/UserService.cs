using Application.Repositories;
using Application.Services;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userValidator)
    {
        _userRepository = userValidator ?? throw new ArgumentNullException(nameof(userValidator));
    }

    public async Task<bool> IsUniqueEmail(string email, CancellationToken cancellationToken)
    {
        return await _userRepository.EmailExists(email, cancellationToken);
    }
}