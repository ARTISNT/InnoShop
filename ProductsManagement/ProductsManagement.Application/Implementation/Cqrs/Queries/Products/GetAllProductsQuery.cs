using MediatR;
using ProductsManagement.Application.Dto;

namespace ProductsManagement.Application.Implementation.Cqrs.Queries.Products;

public record GetAllProductsQuery(ProductFilteringDto ProductFilteringDto) : IRequest<ICollection<ProductResponseDto>>;