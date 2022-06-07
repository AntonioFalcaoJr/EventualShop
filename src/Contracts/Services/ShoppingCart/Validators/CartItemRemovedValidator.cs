using FluentValidation;

namespace Contracts.Services.ShoppingCart.Validators;

public class CartItemRemovedValidator : AbstractValidator<DomainEvent.CartItemRemoved> { }