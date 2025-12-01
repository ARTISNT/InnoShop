using MediatR;
using ProductsManagement.Application.Abstractions;
using ProductsManagement.Application.Implementation.Cqrs.Commands;
using ProductsManagement.Application.Implementation.Cqrs.Commands.Products;

namespace ProductsManagement.Application.Implementation.Cqrs.Handlers.Commands.Products;

public class UpdateStatusOfProductCommandHandler(IProductRepository productRepository, IUserContext userContext) 
    : IRequestHandler<UpdateStatusOfProductCommand>
{
    public async Task Handle(UpdateStatusOfProductCommand request, CancellationToken cancellationToken)
    {
        if(request is null)
            throw new ArgumentNullException(nameof(request));
        
        var product = await productRepository.GetByIdAsync(request.Id);
        
        if(request.Id == Guid.Empty)
            throw new ArgumentException("Id cannot be empty", nameof(request.Id));
        
        var existing = await productRepository.GetByIdAsync(request.Id);
        if(existing is null)
            throw new KeyNotFoundException($"Product with id {request.Id} was not found");
        
        userContext.EnsureOwnerOrAdmin(product.UserId);
        
        await productRepository.UpdateStatusAsync(request.Id, request.Status);
    }
}