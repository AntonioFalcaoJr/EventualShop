using FluentValidation;

namespace ECommerce.Contracts.ShoppingCarts.Validators;

public class CartItemAddedValidator : AbstractValidator<DomainEvent.CartItemAdded> { }