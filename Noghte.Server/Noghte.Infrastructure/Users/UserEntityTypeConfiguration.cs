using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Noghte.Domain.Users;

namespace Noghte.Infrastructure.Users;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.UserName).IsRequired(true);
        builder.Property(x => x.RoleId).IsRequired(true);
        builder.Property(x => x.FirstName).IsRequired(true);
        builder.Property(x => x.PasswordHash).IsRequired(false);
        builder.Property(x => x.Email).IsRequired(false);
        builder.Property(x => x.Bio).IsRequired(false);
        builder.Property(x => x.PhoneNumber).IsRequired(true);

        #region Relations

        builder.HasMany(x => x.Categories)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.Posts)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.Followings)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.FollowerId);

        builder.HasOne(x => x.Role)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.RoleId);

        #endregion
    }
}