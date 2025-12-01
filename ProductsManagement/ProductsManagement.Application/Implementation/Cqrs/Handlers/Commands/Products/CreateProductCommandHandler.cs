using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using ProductsManagement.Application.Abstractions;
using ProductsManagement.Application.Implementation.Cqrs.Commands;
using ProductsManagement.Application.Implementation.Cqrs.Commands.Products;
using ProductsManagement.Domain.Models;

namespace ProductsManagement.Application.Implementation.Cqrs.Handlers.Commands.Products;

public class CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper, IUserContext userContext)
    : IRequestHandler<CreateProductCommand, Guid>
{
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Product;
        if (dto is null)
            throw new ArgumentNullException("Dto cannot be null", nameof(dto));
        
        var product = mapper.Map<ProductEntity>(dto);
        
        var userId = userContext.GetUserId();
        
        product.SetUserId(userId);
        await productRepository.AddAsync(product);
        
        return product.Id;
    }
}