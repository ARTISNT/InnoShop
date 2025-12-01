using FluentValidation;
using ProductsManagement.Api.Dto;

namespace ProductsManagement.Api.Validation;

public class UpdateStatusOfProductApiDtoValidator : AbstractValidator<UpdateStatusOfProductApiDto>
{
    public UpdateStatusOfProductApiDtoValidator()
    {
        RuleFor(x => x.Status)
            .NotEmpty()
            .WithMessage("Status is required");
    }
}