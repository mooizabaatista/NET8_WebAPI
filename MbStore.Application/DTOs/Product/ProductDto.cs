using MbStore.Application.DTOs.Category;

namespace MbStore.Application.DTOs.Product;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }

    // relationships
    public virtual CategoryDto? Category { get; set; }
}
