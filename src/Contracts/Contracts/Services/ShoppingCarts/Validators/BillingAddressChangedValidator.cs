using FluentValidation;

namespace Contracts.Services.ShoppingCarts.Validators;

public class BillingAddressChangedValidator : AbstractValidator<DomainEvent.BillingAddressChanged> { }