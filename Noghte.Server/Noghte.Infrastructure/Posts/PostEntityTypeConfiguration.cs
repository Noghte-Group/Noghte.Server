using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Noghte.Domain.Posts;
using Noghte.Domain.Users;

namespace Noghte.Infrastructure.Posts;

public class PostEntityTypeConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.UserId).IsRequired(true);
        builder.Property(x => x.Title).IsRequired(true);
        builder.Property(x => x.Body).IsRequired(true);
        builder.Property(x => x.Description).IsRequired(true);
        builder.Property(x => x.ImageUrl).IsRequired(true);
        builder.Property(x => x.CreatedAt).IsRequired(true);
        builder.Property(x => x.LikeCount).IsRequired(true);
        builder.Property(x => x.ReadingTime).IsRequired(true);

        #region Relations

        builder.HasMany(x => x.Comments)
            .WithOne(x => x.Post)
            .HasForeignKey(x => x.PostId);
        
        builder.HasMany(x => x.PostToTags)
            .WithOne(x => x.Post)
            .HasForeignKey(x => x.PostId);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Posts)
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Posts)
            .HasForeignKey(x => x.CategoryId);

        #endregion
    }

}