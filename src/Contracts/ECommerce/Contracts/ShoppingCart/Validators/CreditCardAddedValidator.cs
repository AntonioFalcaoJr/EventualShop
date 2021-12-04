using FluentValidation;

namespace ECommerce.Contracts.ShoppingCart.Validators;

public class CreditCardAddedValidator : AbstractValidator<DomainEvents.CreditCardAdded> { }