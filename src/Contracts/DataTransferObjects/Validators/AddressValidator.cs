using FluentValidation;

namespace Contracts.DataTransferObjects.Validators;

public class AddressValidator : AbstractValidator<Dto.Address>
{
    public AddressValidator()
    {
        RuleFor(address => address.City)
            .NotNull()
            .NotEmpty();

        RuleFor(address => address.Country)
            .NotNull()
            .NotEmpty();

        RuleFor(address => address.State)
            .NotNull()
            .NotEmpty();

        RuleFor(address => address.Street)
            .NotNull()
            .NotEmpty();

        RuleFor(address => address.ZipCode)
            .NotNull()
            .NotEmpty();

        When(address => address.Number is not null, () =>
        {
            RuleFor(address => address.Number)
                .GreaterThan(0);
        });

        When(address => address.Complement is not null, () =>
        {
            RuleFor(address => address.Complement)
                .NotEmpty()
                .MaximumLength(80);
        });
    }
}