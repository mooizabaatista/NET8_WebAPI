using System.ComponentModel.DataAnnotations;

namespace MbStore.Application.DTOs.Category;

public class CategoryCreateDto
{
    [Required(ErrorMessage = "The name field is required.")]
    [MaxLength(100, ErrorMessage = "The name field can only contain a maximum of 100 characters.")]
    public string Name { get; set; } = string.Empty;
}
