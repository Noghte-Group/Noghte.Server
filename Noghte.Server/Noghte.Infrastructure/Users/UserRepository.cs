using Noghte.Domain;
using Noghte.Domain.Users;
using Noghte.Infrastructure.ApplicationDbContext;

namespace Noghte.Infrastructure.Users;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly IJwtService _service;

    public UserRepository(NoghteDbContext dbContext, IJwtService service) : base(dbContext)
    {
        _service = service;
    }

    public async Task<string> GenerateUserWithTokenAsync(User entity, CancellationToken cancellationToken, bool saveNow = true)
    {
        var user = await base.AddAsync(entity, cancellationToken, saveNow);

        var token = await _service.GenerateTokenAsync(user);

        return token;
    }
}
