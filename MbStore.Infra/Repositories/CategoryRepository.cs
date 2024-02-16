using MbStore.Domain.Entities;
using MbStore.Infra.Context;
using MbStore.Infra.Interfaces;

namespace MbStore.Infra.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }
}
