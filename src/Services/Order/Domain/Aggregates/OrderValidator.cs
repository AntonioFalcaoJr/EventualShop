using Domain.Abstractions.Validators;
using Domain.Entities.Customers;
using Domain.Entities.OrderItems;
using FluentValidation;

namespace Domain.Aggregates;

public class OrderValidator : EntityValidator<Order, Guid>
{
    public OrderValidator()
    {
        RuleFor(cart => cart.Customer)
            .SetValidator(new CustomerValidator());

        RuleForEach(cart => cart.Items)
            .SetValidator(new OrderItemValidator());

        When(cart => cart.Items.Any(), () =>
        {
            RuleFor(cart => cart.Total)
                .GreaterThan(0);
        });
    }
}