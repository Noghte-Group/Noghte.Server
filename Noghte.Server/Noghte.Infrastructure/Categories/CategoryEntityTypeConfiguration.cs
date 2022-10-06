using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Noghte.Domain.Categories;
using Noghte.Infrastructure.Users;

namespace Noghte.Infrastructure.Categories;

public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).IsRequired(true);
        builder.Property(x => x.ImageUrl).IsRequired(true);

        #region Relations

        builder.HasOne(x => x.User)
            .WithMany(x => x.Categories)
            .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.Posts)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId);

        #endregion
    }
}