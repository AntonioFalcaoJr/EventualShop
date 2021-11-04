using FluentValidation;

namespace Messages.Services.ShoppingCarts.Validators;

public class CartItemRemovedValidator : AbstractValidator<DomainEvents.CartItemRemoved> { }