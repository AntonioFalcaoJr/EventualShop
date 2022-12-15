using FluentValidation;

namespace Contracts.DataTransferObjects.Validators;

public class ProductValidator : AbstractValidator<Dto.Product>
{
    public ProductValidator()
    {
        RuleFor(product => product.Brand)
            .NotNull()
            .NotEmpty();
        
        RuleFor(product => product.Category)
            .NotNull()
            .NotEmpty();
        
        RuleFor(product => product.Description)
            .NotNull()
            .NotEmpty();
        
        RuleFor(product => product.Name)
            .NotNull()
            .NotEmpty();
        
        RuleFor(product => product.Unit)
            .NotNull()
            .NotEmpty();
        
        RuleFor(request => request.Sku)
            .NotNull()
            .NotEmpty();
    }
}