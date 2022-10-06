using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Noghte.Domain.PostToTags;

namespace Noghte.Infrastructure.PostToTags;

public class PostToTagEntityTypeConfiguration : IEntityTypeConfiguration<PostToTag>
{
    public void Configure(EntityTypeBuilder<PostToTag> builder)
    {
        builder.HasKey(x => x.Id);

        #region Relations

        builder.HasOne(x => x.Post)
            .WithMany(x => x.PostToTags)
            .HasForeignKey(x => x.PostId);
        
        builder.HasOne(x => x.Tag)
            .WithMany(x => x.PostToTags)
            .HasForeignKey(x => x.TagId);

        #endregion
    }
}