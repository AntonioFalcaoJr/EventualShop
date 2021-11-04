using FluentValidation;

namespace Messages.Services.ShoppingCarts.Validators;

public class CartCreatedValidator : AbstractValidator<DomainEvents.CartCreated> { }