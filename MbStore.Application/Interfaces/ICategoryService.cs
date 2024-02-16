using MbStore.Application.DTOs.Category;

namespace MbStore.Application.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> Get();
    Task<CategoryDto> Get(Guid id);
    Task<CategoryDto> Create(CategoryCreateDto categoryCreateDto);
    Task<CategoryDto> Update(CategoryUpdateDto categoryUpdateDto);
    Task<bool> Delete(Guid id);
}
