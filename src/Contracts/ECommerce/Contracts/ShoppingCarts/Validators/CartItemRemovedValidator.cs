using FluentValidation;

namespace ECommerce.Contracts.ShoppingCarts.Validators;

public class CartItemRemovedValidator : AbstractValidator<DomainEvents.CartItemRemoved> { }