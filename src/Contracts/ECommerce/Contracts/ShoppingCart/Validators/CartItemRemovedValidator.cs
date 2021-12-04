using FluentValidation;

namespace ECommerce.Contracts.ShoppingCart.Validators;

public class CartItemRemovedValidator : AbstractValidator<DomainEvents.CartItemRemoved> { }