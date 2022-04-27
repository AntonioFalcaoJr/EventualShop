using FluentValidation;

namespace Contracts.Services.ShoppingCart.Validators;

public class BillingAddressChangedValidator : AbstractValidator<DomainEvent.BillingAddressChanged> { }