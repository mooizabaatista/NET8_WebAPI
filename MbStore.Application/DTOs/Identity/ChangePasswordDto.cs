using System.ComponentModel.DataAnnotations;

namespace MbStore.Application.DTOs.Identity;

public class ChangePasswordDto
{
    [Required(ErrorMessage = "The username field is required.")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "The current password field is required.")]
    public string CurrentPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "The new password field is required.")]
    public string Newpassword { get; set; } = string.Empty;
}
