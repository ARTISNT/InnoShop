using AutoMapper;
using ProductsManagement.Application.Dto;
using ProductsManagement.Domain.Models;

namespace ProductsManagement.Application.Common.AutoMapper;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<ProductEntity, ProductResponseDto>();
        CreateMap<CreateProductDto, ProductEntity>();
        CreateMap<UpdateProductDto, ProductEntity>();
    }
}