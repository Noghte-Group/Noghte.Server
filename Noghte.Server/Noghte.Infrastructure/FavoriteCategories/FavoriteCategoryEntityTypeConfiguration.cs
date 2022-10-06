using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Noghte.Domain.FavoriteCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noghte.Infrastructure.FavoriteCategories;

public class FavoriteCategoryEntityTypeConfiguration : IEntityTypeConfiguration<FavoriteCategory>
{
    public void Configure(EntityTypeBuilder<FavoriteCategory> builder)
    {
        builder.HasKey(x => x.Id);

        #region Relations
        builder.HasOne(x => x.User)
            .WithMany(x => x.FavoriteCategories)
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Category)
            .WithMany(x => x.FavoriteCategories)
            .HasForeignKey(x => x.CategoryId);

        #endregion
    }
}
