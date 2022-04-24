using FluentValidation;

namespace ECommerce.Contracts.ShoppingCarts.Validators;

public class CreditCardAddedValidator : AbstractValidator<DomainEvent.CreditCardAdded> { }