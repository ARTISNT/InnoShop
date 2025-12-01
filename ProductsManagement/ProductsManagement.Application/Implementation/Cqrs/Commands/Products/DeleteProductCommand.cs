using MediatR;

namespace ProductsManagement.Application.Implementation.Cqrs.Commands.Products;

public record DeleteProductCommand(Guid Id) : IRequest;