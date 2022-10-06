using Microsoft.EntityFrameworkCore;
using Noghte.BuildingBlock.Common;
using Noghte.BuildingBlock.Utilities;
using Noghte.Domain.Users;

namespace Noghte.Infrastructure.ApplicationDbContext;

public class NoghteDbContext : DbContext
{
    public NoghteDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var entitiesAssembly = typeof(User).Assembly;

        modelBuilder.RegisterAllEntities<IEntity>(entitiesAssembly);
        modelBuilder.RegisterEntityTypeConfiguration(entitiesAssembly);
        modelBuilder.AddRestrictDeleteBehaviorConvention();
    }
}