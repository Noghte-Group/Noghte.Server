using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Noghte.Domain.Bookmarks;

namespace Noghte.Infrastructure.Bookmarks;

public class BookmarkEntityTypeConfiguration : IEntityTypeConfiguration<Bookmark>
{
    public void Configure(EntityTypeBuilder<Bookmark> builder)
    {
        builder.HasKey(x => x.Id);

        #region Relations

        builder.HasOne(x => x.Post)
            .WithMany(x => x.Bookmarks)
            .HasForeignKey(x => x.PostId);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Bookmarks)
            .HasForeignKey(x => x.UserId);

        #endregion
    }
}