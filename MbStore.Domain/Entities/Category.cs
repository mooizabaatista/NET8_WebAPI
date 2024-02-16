using MbStore.Domain.Entities.Base;

namespace MbStore.Domain.Entities;

public class Category : EntityBase
{
    public string Name { get; set; } = string.Empty;

    // relationships
    public virtual IEnumerable<Product>? Products { get; set; }
}
