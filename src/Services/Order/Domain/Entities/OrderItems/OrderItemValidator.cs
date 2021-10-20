using System;
using Domain.Abstractions.Validators;

namespace Domain.Entities.OrderItems
{
    public class OrderItemValidator : EntityValidator<OrderItem, Guid> { }
}