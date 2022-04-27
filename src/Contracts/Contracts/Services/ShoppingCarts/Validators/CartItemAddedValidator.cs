using FluentValidation;

namespace Contracts.Services.ShoppingCarts.Validators;

public class CartItemAddedValidator : AbstractValidator<DomainEvent.CartItemAdded> { }