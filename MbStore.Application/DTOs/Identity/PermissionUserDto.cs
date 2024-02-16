using System.ComponentModel.DataAnnotations;

namespace MbStore.Application.DTOs.Identity;

public class PermissionUserDto
{
    [Required(ErrorMessage = "The username field is required.")]
    public string UserName { get; set; } = string.Empty;
}
