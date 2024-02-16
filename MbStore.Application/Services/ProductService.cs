using AutoMapper;
using MbStore.Application.DTOs.Product;
using MbStore.Application.Interfaces;
using MbStore.Domain.Entities;
using MbStore.Infra.Interfaces;
using System.Xml.XPath;

namespace MbStore.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository repository, IMapper mapper, ICategoryRepository categoryRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<ProductDto>> Get()
    {
        try
        {
            var productsEntity = await _repository.GetAsync();
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(productsEntity);
            return productsDto;
        }
        catch (Exception ex)
        {
             throw new Exception(ex.Message);
        }
    }

    public async Task<ProductDto> Get(Guid id)
    {
        try
        {
            var productEntity = await _repository.GetAsync(id);
            var productDto = _mapper.Map<ProductDto>(productEntity);
            return productDto;
        }
        catch (Exception ex)
        {
             throw new Exception(ex.Message);
        }
    }

    public async Task<ProductDto> Create(ProductCreateDto productCreateDto)
    {
        try
        {
            var category = await _categoryRepository.GetAsync(productCreateDto.CategoryId);
            if (category == null)
                throw new Exception($"Category with id:{productCreateDto.CategoryId} not found");

            var productEntity = _mapper.Map<Product>(productCreateDto);
            var resultCreate = await _repository.CreateAsync(productEntity);
            var resultDto = _mapper.Map<ProductDto>(resultCreate);
            return resultDto;
        }
        catch (Exception ex)
        {
             throw new Exception(ex.Message);
        }
    }

    public async Task<ProductDto> Update(ProductUpdateDto productUpdateDto)
    {
        try
        {
            var productEntity = _mapper.Map<Product>(productUpdateDto);
            var resultUpdate = await _repository.UpdateAsync(productEntity);

            var category = await _categoryRepository.GetAsync(resultUpdate.CategoryId);
            resultUpdate.Category = category;

            var resultDto = _mapper.Map<ProductDto>(resultUpdate);
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
