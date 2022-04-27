using FluentValidation;

namespace Contracts.Services.ShoppingCart.Validators;

public class CartItemAddedValidator : AbstractValidator<DomainEvent.CartItemAdded> { }