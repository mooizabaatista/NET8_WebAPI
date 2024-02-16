using AutoMapper;
using MbStore.Application.DTOs.Category;
using MbStore.Application.DTOs.Product;
using MbStore.Domain.Entities;

namespace MbStore.Application.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Category
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Category, CategoryCreateDto>().ReverseMap();
        CreateMap<Category, CategoryUpdateDto>().ReverseMap();
        CreateMap<CategoryDto, CategoryCreateDto>().ReverseMap();
        CreateMap<CategoryDto, CategoryUpdateDto>().ReverseMap();
        CreateMap<CategoryCreateDto, CategoryUpdateDto>().ReverseMap();

        // Product
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Product, ProductCreateDto>().ReverseMap();
        CreateMap<Product, ProductUpdateDto>().ReverseMap();
        CreateMap<ProductDto, ProductCreateDto>().ReverseMap();
        CreateMap<ProductDto, ProductUpdateDto>().ReverseMap();
        CreateMap<ProductCreateDto, ProductUpdateDto>().ReverseMap();
    }
}
