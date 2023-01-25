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

        When(order => order.Items.Any(item => item.IsDeleted is false), () =>
        {
            RuleFor(order => order.Total.Amount)
                .GreaterThan(0);
        });
    }
}