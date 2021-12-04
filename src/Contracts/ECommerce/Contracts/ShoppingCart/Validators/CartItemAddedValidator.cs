using FluentValidation;

namespace ECommerce.Contracts.ShoppingCart.Validators;

public class CartItemAddedValidator : AbstractValidator<DomainEvents.CartItemAdded> { }