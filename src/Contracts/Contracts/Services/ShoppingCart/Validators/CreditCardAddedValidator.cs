using FluentValidation;

namespace Contracts.Services.ShoppingCart.Validators;

public class CreditCardAddedValidator : AbstractValidator<DomainEvent.CreditCardAdded> { }