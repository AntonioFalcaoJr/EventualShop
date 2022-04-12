using FluentValidation;

namespace ECommerce.Contracts.ShoppingCarts.Validators;

public class CartCreatedValidator : AbstractValidator<DomainEvents.CartCreated> { }