using Contracts.DataTransferObjects;
using FluentValidation;

namespace WebAPI.APIs.Accounts.Validators;

// TODO [Obsolete("Use AddressValidator from Contracts library")]
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
    }
}