using MbStore.Infra.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MbStore.Infra.Mappings.Identity;

public class UserExtendedMap : IEntityTypeConfiguration<UserExtended>
{
    private readonly UserManager<UserExtended> _userManager;

    public UserExtendedMap(UserManager<UserExtended> userManager)
    {
        _userManager = userManager;
    }

    public void Configure(EntityTypeBuilder<UserExtended> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(100);
    }
}
