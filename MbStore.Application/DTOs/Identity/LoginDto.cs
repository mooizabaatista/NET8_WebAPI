using System.ComponentModel.DataAnnotations;

namespace MbStore.Application.DTOs.Identity;

public class LoginDto
{
    [Required(ErrorMessage = "The username field is required.")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "The password field is required")]
    public string Password { get; set; } = string.Empty;
}
