using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Noghte.Domain.Comments;
using Noghte.Domain.Tags;

namespace Noghte.Infrastructure.Tags;

public class TagEntityTypeConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).IsRequired(true);

        #region Relations

        builder.HasMany(x => x.PostToTags)
            .WithOne(x => x.Tag)
            .HasForeignKey(x => x.TagId);


        #endregion
    }
}