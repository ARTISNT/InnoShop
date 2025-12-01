using AutoMapper;
using MediatR;
using ProductsManagement.Application.Abstractions;
using ProductsManagement.Application.Implementation.Cqrs.Commands;
using ProductsManagement.Application.Implementation.Cqrs.Commands.Products;
using ProductsManagement.Domain.Models;

namespace ProductsManagement.Application.Implementation.Cqrs.Handlers.Commands.Products;

public class UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper, IUserContext userContext) 
    : IRequestHandler<UpdateProductCommand, Guid>
{
    public async Task<Guid> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var dto = request.UpdateProductDto;
        if (dto is null)
            throw new ArgumentNullException("Dto cannot be null", nameof(dto));
        
        if(request.Id == Guid.Empty)
            throw new ArgumentException("Id cannot be empty", nameof(request.Id));
        
        var existing = await productRepository.GetByIdAsync(request.Id);
        if(existing is null)
            throw new KeyNotFoundException($"Product with id {request.Id} was not found");
            
        var updateProduct = mapper.Map<ProductEntity>(dto);
        
        userContext.EnsureOwnerOrAdmin(existing.UserId);
        
        await productRepository.UpdateAsync(request.Id, updateProduct);
        return updateProduct.Id;
    }
}