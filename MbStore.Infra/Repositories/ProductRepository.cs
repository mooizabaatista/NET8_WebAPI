using MbStore.Domain.Entities;
using MbStore.Infra.Context;
using MbStore.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MbStore.Infra.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Product>> GetAsync()
    {
		try
		{
            return await _context.Products.Include(x => x.Category).ToListAsync();
		}
		catch (Exception ex)
		{
             throw new Exception(ex.Message);
		}
    }

    public override async Task<Product> GetAsync(Guid id)
    {
        try
        {
            var product = await _context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                throw new Exception($"Product with ID {id} not found.");
            }
            return product;
        }
        catch (Exception ex)
        {
             throw new Exception(ex.Message);
        }
    }
}
