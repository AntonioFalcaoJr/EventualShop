using FluentValidation;

namespace Contracts.Services.Identities.Validators;

public class UserRegisteredValidator : AbstractValidator<DomainEvent.UserRegistered>
{
    public UserRegisteredValidator()
    {

    }
}