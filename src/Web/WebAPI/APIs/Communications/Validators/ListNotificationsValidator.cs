using FluentValidation;

namespace WebAPI.APIs.Communications.Validators;

public class ListNotificationsValidator : AbstractValidator<Requests.ListNotifications>
{
    public ListNotificationsValidator()
    {
        RuleFor(request => request.Limit)
            .GreaterThan(0)
            .LessThanOrEqualTo(100);

        RuleFor(request => request.Offset)
            .GreaterThan(0);
    }
}