using System.Security.Claims;
using MediatR;
using ProductsManagement.Application.Dto;

namespace ProductsManagement.Application.Implementation.Cqrs.Commands.Products;

public record UpdateProductCommand(Guid Id, ClaimsPrincipal User, UpdateProductDto UpdateProductDto) : IRequest<Guid>;