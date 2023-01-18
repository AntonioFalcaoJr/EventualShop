using FluentValidation;

namespace WebAPI.APIs.Payments.Validators;

public class ListPaymentMethodListItemValidator : AbstractValidator<Queries.ListPaymentMethodListItem>
{
    public ListPaymentMethodListItemValidator()
    {
        RuleFor(request => request.PaymentId)
            .NotEmpty();

        RuleFor(request => request.Limit)
            .GreaterThan(0)
            .LessThanOrEqualTo(100);

        RuleFor(request => request.Offset)
            .GreaterThanOrEqualTo(0);
    }
}