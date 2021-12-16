using FluentValidation;

namespace ECommerce.Contracts.Common.Validators;

public class ItemValidator : AbstractValidator<Models.Item>
{
    public ItemValidator()
    {
        RuleFor(item => item.Quantity)
            .GreaterThan(0);
    }
}