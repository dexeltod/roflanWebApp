namespace Application.Services;

public interface IUserService
{
    Task<bool> IsUniqueEmail(string email, CancellationToken cancellationToken);
}