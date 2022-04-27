using FluentValidation;

namespace Contracts.Services.ShoppingCarts.Validators;

public class CartCreatedValidator : AbstractValidator<DomainEvent.CartCreated> { }