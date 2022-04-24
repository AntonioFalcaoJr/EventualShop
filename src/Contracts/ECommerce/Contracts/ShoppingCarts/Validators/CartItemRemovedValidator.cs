using FluentValidation;

namespace ECommerce.Contracts.ShoppingCarts.Validators;

public class CartItemRemovedValidator : AbstractValidator<DomainEvent.CartItemRemoved> { }