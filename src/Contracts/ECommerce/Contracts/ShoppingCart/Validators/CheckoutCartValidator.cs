using FluentValidation;

namespace ECommerce.Contracts.ShoppingCart.Validators;

public class CheckoutCartValidator : AbstractValidator<Commands.CheckOutCart> { }