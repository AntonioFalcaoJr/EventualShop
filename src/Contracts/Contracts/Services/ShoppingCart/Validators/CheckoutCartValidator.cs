using FluentValidation;

namespace Contracts.Services.ShoppingCart.Validators;

public class CheckoutCartValidator : AbstractValidator<Command.CheckOutCart> { }