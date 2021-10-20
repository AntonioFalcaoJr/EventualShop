using System;
using System.Linq;
using Domain.Abstractions.Validators;
using Domain.Entities.OrderItems;
using FluentValidation;

namespace Domain.Aggregates
{
    public class OrderValidator : EntityValidator<Order, Guid>
    {
        public OrderValidator()
        {
            RuleFor(cart => cart.UserId)
                .NotEqual(Guid.Empty);

            RuleForEach(cart => cart.Items)
                .SetValidator(new OrderItemValidator());

            When(cart => cart.Items.Any(), () =>
            {
                RuleFor(cart => cart.Total)
                    .GreaterThan(0);
            });
        }
    }
}