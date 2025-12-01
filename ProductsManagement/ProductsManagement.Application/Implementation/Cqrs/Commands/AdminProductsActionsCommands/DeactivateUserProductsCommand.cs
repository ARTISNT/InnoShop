using MediatR;

namespace ProductsManagement.Application.Implementation.Cqrs.Commands.AdminProductsActionsCommands;

public record DeactivateUserProductsCommand(Guid UserId) :  IRequest;