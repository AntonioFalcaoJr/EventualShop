using FluentValidation;

namespace Contracts.Services.Account.Validators;

public class CreateAccountValidator : AbstractValidator<Command.CreateAccount> { }