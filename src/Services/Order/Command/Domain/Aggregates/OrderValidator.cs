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

        When(cart => cart.Items.Any(item => item.IsDeleted is false), () =>
        {
            RuleFor(cart => cart.Total.Value)
                .GreaterThan(0);
        });
    }
}