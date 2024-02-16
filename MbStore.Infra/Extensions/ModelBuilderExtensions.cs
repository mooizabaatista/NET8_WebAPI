using MbStore.Infra.Identity;
using MbStore.Utils.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MbStore.Infra.Extensions;

public static class ModelBuilderExtensions
{
    public static void SeedRolesAndUserAdmin(this ModelBuilder modelbuilder)
    {
        // Add Roles
        List<IdentityRole> roles = new List<IdentityRole>()
        {
            new IdentityRole { Name = SharedConstants.ADMIN, NormalizedName = SharedConstants.ADMIN},
            new IdentityRole { Name = SharedConstants.OWNER, NormalizedName = SharedConstants.OWNER},
            new IdentityRole { Name = SharedConstants.USER, NormalizedName = SharedConstants.USER}
        };
        modelbuilder.Entity<IdentityRole>().HasData(roles);


        // Add User
        var passwordHasher = new PasswordHasher<UserExtended>();
        UserExtended userAdmin = new UserExtended
        {
            FirstName = "Administrador",
            LastName = "Administrador do sistema",
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Email = "admin@admin.com",
            NormalizedEmail = "ADMIN@ADMIN.COM",
        };
        modelbuilder.Entity<UserExtended>().HasData(userAdmin);

        // Add password to user
        userAdmin.PasswordHash = passwordHasher.HashPassword(userAdmin, "admin");

        // add roles to user
        List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>();

        userRoles.Add(new IdentityUserRole<string>
        {
            UserId = userAdmin.Id,
            RoleId = roles.First(x => x.Name == SharedConstants.ADMIN).Id
        });

        userRoles.Add(new IdentityUserRole<string>
        {
            UserId = userAdmin.Id,
            RoleId = roles.First(x => x.Name == SharedConstants.OWNER).Id
        });

        userRoles.Add(new IdentityUserRole<string>
        {
            UserId = userAdmin.Id,
            RoleId = roles.First(x => x.Name == SharedConstants.USER).Id
        });

        modelbuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);
    }
}
