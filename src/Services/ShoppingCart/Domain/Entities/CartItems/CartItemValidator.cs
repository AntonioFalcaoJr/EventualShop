using System;
using Domain.Abstractions.Validators;

namespace Domain.Entities.CartItems;

public class CartItemValidator : EntityValidator<CartItem, Guid> { }