using Noghte.Domain.Users;

namespace Noghte.Domain;

public interface IJwtService
{
    Task<string> GenerateTokenAsync(User user);
}
