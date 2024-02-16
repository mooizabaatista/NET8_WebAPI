using System.ComponentModel.DataAnnotations;

namespace MbStore.Application.DTOs.Category;

public class CategoryUpdateDto
{
    [Required(ErrorMessage = "The id field is required.")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "The name field is required.")]
    [MaxLength(100, ErrorMessage = "The name field can only contain a maximum of 100 characters.")]
    public string Name { get; set; } = string.Empty;
}
