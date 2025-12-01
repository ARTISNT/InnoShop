using MediatR;
using ProductsManagement.Application.Abstractions;
using ProductsManagement.Application.Implementation.Cqrs.Commands;
using ProductsManagement.Application.Implementation.Cqrs.Commands.Products;

namespace ProductsManagement.Application.Implementation.Cqrs.Handlers.Commands.Products;

public class DeleteProductCommandHandler(IProductRepository productRepository, IUserContext userContext) : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        if(request.Id == Guid.Empty)
            throw new ArgumentException("Id cannot be empty", nameof(request.Id));
        
        var existing = await productRepository.GetByIdAsync(request.Id);
        if(existing is null)
            throw new KeyNotFoundException($"Product with id {request.Id} was not found");
        
        userContext.EnsureOwnerOrAdmin(existing.UserId);
        
        await productRepository.DeleteAsync(request.Id);
    }
}