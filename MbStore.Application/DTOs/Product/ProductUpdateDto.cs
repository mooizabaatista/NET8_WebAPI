using MbStore.Application.DTOs.Category;
using System.ComponentModel.DataAnnotations;

namespace MbStore.Application.DTOs.Product;

public class ProductUpdateDto
{
    [Required(ErrorMessage = "The id field is required.")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "The name field is required.")]
    [MaxLength(100, ErrorMessage = "The name field can only contain a maximum of 100 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "The price field is required.")]
    [Range(1, double.MaxValue, ErrorMessage = "The price must be at least 1")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "The stock field is required.")]
    public int Stock { get; set; }

    // relationships
    [Required(ErrorMessage = "The categoryId field is required.")]
    public Guid CategoryId { get; set; }
}
