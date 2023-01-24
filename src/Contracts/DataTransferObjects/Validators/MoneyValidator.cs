using FluentValidation;

namespace Contracts.DataTransferObjects.Validators;

public class MoneyValidator : AbstractValidator<Dto.Money>
{
    public MoneyValidator()
    {
        RuleFor(money => money.Amount)
            .GreaterThan("0");

        RuleFor(money => money.Currency)
            .NotEmpty();
    }
}