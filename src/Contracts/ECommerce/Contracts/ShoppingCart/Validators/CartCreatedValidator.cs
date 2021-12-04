using FluentValidation;

namespace ECommerce.Contracts.ShoppingCart.Validators;

public class CartCreatedValidator : AbstractValidator<DomainEvents.CartCreated> { }