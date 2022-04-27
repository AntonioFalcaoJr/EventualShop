using FluentValidation;

namespace Contracts.Services.ShoppingCarts.Validators;

public class CartItemRemovedValidator : AbstractValidator<DomainEvent.CartItemRemoved> { }