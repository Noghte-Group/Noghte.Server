using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Noghte.Domain.Comments;
using Noghte.Domain.Posts;
using Noghte.Domain.Users;

namespace Noghte.Infrastructure.Comments;

public class CommentEntityTypeConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Body).IsRequired(true);
        builder.Property(x => x.CreatedAt).IsRequired(true);
        builder.Property(x => x.LikeCount).IsRequired(true);

    #region Relations

    builder.HasOne(x => x.Post)
        .WithMany(x => x.Comments)
        .HasForeignKey(x => x.PostId);
    
    builder.HasOne(x => x.User)
        .WithMany(x => x.Comments)
        .HasForeignKey(x => x.UserId);

    #endregion
    }

}