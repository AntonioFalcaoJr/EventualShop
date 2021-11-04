using FluentValidation;

namespace Messages.Services.ShoppingCarts.Validators;

public class CartItemAddedValidator : AbstractValidator<DomainEvents.CartItemAdded> { }