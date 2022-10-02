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
        builder.Property(x => x.FirstName).IsRequired(true);
        builder.Property(x => x.PhoneNumber).IsRequired(true);

        #region Relations

        // builder.HasOne(x=> x.Categories)
        //     .WithMany()
        //     .HasForeignKey(x=> x.)

        #endregion
    }
}