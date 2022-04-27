using FluentValidation;

namespace Contracts.Services.ShoppingCarts.Validators;

public class CreditCardAddedValidator : AbstractValidator<DomainEvent.CreditCardAdded> { }