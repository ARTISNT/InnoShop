using System.Security.Claims;
using MediatR;
using ProductsManagement.Application.Dto;

namespace ProductsManagement.Application.Implementation.Cqrs.Commands.Products;

public record CreateProductCommand(CreateProductDto Product, ClaimsPrincipal User) : IRequest<Guid>;