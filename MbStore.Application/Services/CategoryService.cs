using AutoMapper;
using MbStore.Application.DTOs.Category;
using MbStore.Application.Interfaces;
using MbStore.Domain.Entities;
using MbStore.Infra.Interfaces;

namespace MbStore.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDto>> Get()
    {
        try
        {
            var categoriesEntity = await _repository.GetAsync();
            var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categoriesEntity);
            return categoriesDto;
        }
        catch (Exception ex)
        {
             throw new Exception(ex.Message);
        }
    }

    public async Task<CategoryDto> Get(Guid id)
    {
        try
        {
            var categoryEntity = await _repository.GetAsync(id);
            var categoryDto = _mapper.Map<CategoryDto>(categoryEntity);
            return categoryDto;
        }
        catch (Exception ex)
        {
             throw new Exception(ex.Message);
        }
    }

    public async Task<CategoryDto> Create(CategoryCreateDto categoryCreateDto)
    {
        try
        {
            var categoryEntity = _mapper.Map<Category>(categoryCreateDto);
            var resultCreate = await _repository.CreateAsync(categoryEntity);
            var resultDto = _mapper.Map<CategoryDto>(resultCreate);
            return resultDto;
        }
        catch (Exception ex)
        {
             throw new Exception(ex.Message);
        }
    }

    public async Task<CategoryDto> Update(CategoryUpdateDto categoryUpdateDto)
    {
        try
        {
            var categoryEntity = _mapper.Map<Category>(categoryUpdateDto);
            var resultUpdate = await _repository.UpdateAsync(categoryEntity);
            var resultDto = _mapper.Map<CategoryDto>(resultUpdate);
            return resultDto;
        }
        catch (Exception ex)
        {
             throw new Exception(ex.Message);
        }
    }

    public async Task<bool> Delete(Guid id)
    {
        try
        {
            return await _repository.DeleteAsync(id);
        }
        catch (Exception ex)
        {
             throw new Exception(ex.Message);
        }
    }
}
