using ProductsManagement.Application.Dto;
using ProductsManagement.Domain.Models;

namespace ProductsManagement.Infrastructure.Extensions;

public static class ProductFilteringExtension
{
    public static IQueryable<ProductEntity> ApplyFiltering
        (this IQueryable<ProductEntity> query, ProductFilteringDto productFilteringDto)
    {
        query = query.Where(p => !p.IsDeleted);

        if (!string.IsNullOrEmpty(productFilteringDto.Name))
            query = query.Where(p => p.Name.Contains(productFilteringDto.Name));

        if (productFilteringDto.LowerPrice.HasValue)
            query = query.Where(p => p.Price >= productFilteringDto.LowerPrice);

        if (productFilteringDto.UpperPrice.HasValue)
            query = query.Where(p => p.Price <= productFilteringDto.UpperPrice);

        return query;

    }
}