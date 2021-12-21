﻿using System;
using FluentValidation;

namespace ECommerce.Contracts.Account.Validators;

public class CreateAccountValidator : AbstractValidator<Commands.CreateAccount>
{
    public CreateAccountValidator()
    {
        RuleFor(account => account.UserId)
            .NotEqual(default(Guid));

        RuleFor(account => account.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress();

        RuleFor(account => account.FirstName)
            .NotNull()
            .NotEmpty();
    }
}