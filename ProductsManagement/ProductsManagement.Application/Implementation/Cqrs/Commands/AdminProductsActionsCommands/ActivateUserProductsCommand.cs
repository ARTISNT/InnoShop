using MediatR;

namespace ProductsManagement.Application.Implementation.Cqrs.Commands.AdminProductsActionsCommands;

public record ActivateUserProductsCommand(Guid UserId) : IRequest;
