using FluentValidation;

namespace Domain.ValueObjects.Addresses;

public class AddressValidator : AbstractValidator<Address?>
{
    public AddressValidator()
        => When(address => address is not null, () =>
        {
            RuleFor(address => address!.City)
                .NotNull()
                .NotEmpty();

            RuleFor(address => address!.Country)
                .NotNull()
                .NotEmpty();

            RuleFor(address => address!.State)
                .NotNull()
                .NotEmpty();

            RuleFor(address => address!.Street)
                .NotNull()
                .NotEmpty();

            RuleFor(address => address!.ZipCode)
                .NotNull()
                .NotEmpty();
        });
}