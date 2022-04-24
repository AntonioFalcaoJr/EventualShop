using FluentValidation;

namespace ECommerce.Contracts.ShoppingCarts.Validators;

public class CheckoutCartValidator : AbstractValidator<Command.CheckOutCart> { }