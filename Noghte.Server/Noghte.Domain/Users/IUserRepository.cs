namespace Noghte.Domain.Users;

public interface IUserRepository : IGenericRepository<User>
{
    Task<string> GenerateUserWithTokenAsync(User entity, CancellationToken cancellationToken, bool saveNow = true);
}