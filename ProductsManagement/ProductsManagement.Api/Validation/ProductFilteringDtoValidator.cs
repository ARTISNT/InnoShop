using FluentValidation;
using ProductsManagement.Application.Dto;

namespace ProductsManagement.Api.Validation;

public class ProductFilteringDtoValidator : AbstractValidator<ProductFilteringDto>
{
    public ProductFilteringDtoValidator()
    {
        RuleFor(x => x.LowerPrice)
            .GreaterThan(0); 
        
         RuleFor(x => x.UpperPrice)
            .GreaterThan(0);
    }
}