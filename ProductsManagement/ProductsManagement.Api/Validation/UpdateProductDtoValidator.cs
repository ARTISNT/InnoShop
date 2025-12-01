using FluentValidation;
using ProductsManagement.Application.Dto;

namespace ProductsManagement.Api.Validation;

public class UpdateProductDtoValidator  : AbstractValidator<UpdateProductDto>
{
    public UpdateProductDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MinimumLength(3);
        
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required")
            .MinimumLength(10);

        RuleFor(x => x.Price)
            .NotEmpty()
            .WithMessage("Price is required")
            .GreaterThan(0);

        RuleFor(x => x.Available)
            .NotEmpty()
            .WithMessage("Available is required");
    }
}