using MediatR;

namespace ProductsManagement.Application.Implementation.Cqrs.Commands.Products;

public record UpdateStatusOfProductCommand(Guid Id, bool Status) : IRequest;