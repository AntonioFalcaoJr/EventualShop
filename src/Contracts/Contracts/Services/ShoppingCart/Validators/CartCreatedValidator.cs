using FluentValidation;

namespace Contracts.Services.ShoppingCart.Validators;

public class CartCreatedValidator : AbstractValidator<DomainEvent.CartCreated> { }