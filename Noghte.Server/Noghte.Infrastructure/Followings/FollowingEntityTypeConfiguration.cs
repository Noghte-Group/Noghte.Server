using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Noghte.Domain.Comments;
using Noghte.Domain.Followings;

namespace Noghte.Infrastructure.Followings;

public class FollowingEntityTypeConfiguration : IEntityTypeConfiguration<Following>
{
    public void Configure(EntityTypeBuilder<Following> builder)
    {
        builder.HasKey(x => x.Id);

        #region Relations

        builder.HasOne(x => x.User)
            .WithMany(x => x.Followings)
            .HasForeignKey(x => x.FollowerId);

        #endregion
    }
}