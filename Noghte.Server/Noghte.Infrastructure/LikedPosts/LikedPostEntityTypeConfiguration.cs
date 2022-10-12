using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Noghte.Domain.LikedPosts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noghte.Infrastructure.LikedPosts
{
    internal class LikedPostEntityTypeConfiguration : IEntityTypeConfiguration<LikedPost>
    {

        public void Configure(EntityTypeBuilder<LikedPost> builder)
        {
            builder.HasKey(x => x.Id);

            #region Relations

            builder.HasOne(x => x.Post)
                .WithMany(x => x.LikedPosts)
                .HasForeignKey(x => x.PostId);


            builder.HasOne(x => x.Users)
                .WithMany(x => x.LikedPosts)
                .HasForeignKey(x => x.UserId);


            #endregion
        }
    }

}