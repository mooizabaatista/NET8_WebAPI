using MbStore.Application.DTOs.Product;

namespace MbStore.Application.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> Get();
    Task<ProductDto> Get(Guid id);
    Task<ProductDto> Create(ProductCreateDto productCreateDto);
    Task<ProductDto> Update(ProductUpdateDto productUpdateDto);
    Task<bool> Delete(Guid id);
}
