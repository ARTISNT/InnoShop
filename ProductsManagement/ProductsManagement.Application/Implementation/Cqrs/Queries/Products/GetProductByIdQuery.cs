using MediatR;
using ProductsManagement.Application.Dto;

namespace ProductsManagement.Application.Implementation.Cqrs.Queries.Products;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductResponseDto>;