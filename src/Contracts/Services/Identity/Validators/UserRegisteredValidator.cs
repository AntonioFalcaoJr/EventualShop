using FluentValidation;

namespace Contracts.Services.Identity.Validators;

public class UserRegisteredValidator : AbstractValidator<DomainEvent.UserRegistered>
{
    public UserRegisteredValidator()
    {

    }
}