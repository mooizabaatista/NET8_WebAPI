using Microsoft.AspNetCore.Identity;

namespace MbStore.Infra.Identity;

public class UserExtended : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
