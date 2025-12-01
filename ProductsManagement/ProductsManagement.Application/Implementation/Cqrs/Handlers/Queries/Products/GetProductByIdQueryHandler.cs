using AutoMapper;
using MediatR;
using ProductsManagement.Application.Abstractions;
using ProductsManagement.Application.Dto;
using ProductsManagement.Application.Implementation.Cqrs.Queries;
using ProductsManagement.Application.Implementation.Cqrs.Queries.Products;

namespace ProductsManagement.Application.Implementation.Cqrs.Handlers.Queries.Products;

public class GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
    : IRequestHandler<GetProductByIdQuery, ProductResponseDto>
{
    public async Task<ProductResponseDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id);
        if (product is null)
            throw new KeyNotFoundException("Product not found");
        
        return mapper.Map<ProductResponseDto>(product);
    }
}