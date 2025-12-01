using AutoMapper;
using MediatR;
using ProductsManagement.Application.Abstractions;
using ProductsManagement.Application.Dto;
using ProductsManagement.Application.Implementation.Cqrs.Queries;
using ProductsManagement.Application.Implementation.Cqrs.Queries.Products;

namespace ProductsManagement.Application.Implementation.Cqrs.Handlers.Queries.Products;

public class GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
    : IRequestHandler<GetAllProductsQuery, ICollection<ProductResponseDto>>
{
    public async Task<ICollection<ProductResponseDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAllAsync(request.ProductFilteringDto);
        if(products is null)
            throw new KeyNotFoundException("Products not found");
        
        return mapper.Map<ICollection<ProductResponseDto>>(products);
    }
}