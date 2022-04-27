using FluentValidation;

namespace Contracts.Services.ShoppingCarts.Validators;

public class CheckoutCartValidator : AbstractValidator<Command.CheckOutCart> { }