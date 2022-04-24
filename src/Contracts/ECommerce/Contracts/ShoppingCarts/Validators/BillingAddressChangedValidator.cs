using FluentValidation;

namespace ECommerce.Contracts.ShoppingCarts.Validators;

public class BillingAddressChangedValidator : AbstractValidator<DomainEvent.BillingAddressChanged> { }