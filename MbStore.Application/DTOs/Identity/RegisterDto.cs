using System.ComponentModel.DataAnnotations;

namespace MbStore.Application.DTOs.Identity;

public class RegisterDto
{
    [Required(ErrorMessage = "The first name field is required.")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "The last name field is required.")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "The username field is required.")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "The email field is required.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "The password field is required.")]
    [MinLength(8)]
    public string Password { get; set; } = string.Empty;
}
