using MediatR;
using ProductsManagement.Application.Abstractions;
using ProductsManagement.Application.Implementation.Cqrs.Commands;
using ProductsManagement.Application.Implementation.Cqrs.Commands.AdminProductsActionsCommands;

namespace ProductsManagement.Application.Implementation.Cqrs.Handlers.Commands.AdminProductActions;

public class DeactivateProductCommandHandler(IProductRepository productRepository) : IRequestHandler<DeactivateUserProductsCommand>
{
    public async Task Handle(DeactivateUserProductsCommand request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;
        
        if (userId == Guid.Empty)
            throw new ArgumentException("UserId should not be empty", nameof(request.UserId));

        await productRepository.SoftDeleteByUserIdAsync(userId);
    }
}