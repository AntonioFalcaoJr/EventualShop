using Domain.Entities.OrderItems;
using FluentValidation;

namespace Domain.Aggregates;

public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
        RuleFor(order => order.Id)
            .NotEmpty();
        
        RuleForEach(order => order.Items)
            .SetValidator(new OrderItemValidator());

        When(order => order.Items.Any(), () =>
        {
            RuleFor(order => order.Total)
                .GreaterThan(0);
        });
    }
}