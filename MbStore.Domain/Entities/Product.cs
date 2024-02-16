using MbStore.Domain.Entities.Base;

namespace MbStore.Domain.Entities;

public class Product : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }

    // relationships
    public Guid CategoryId { get; set; }
    public virtual Category? Category { get; set; }
}
