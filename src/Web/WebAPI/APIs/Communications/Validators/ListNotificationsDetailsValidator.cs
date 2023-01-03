using FluentValidation;

namespace WebAPI.APIs.Communications.Validators;

public class ListNotificationsDetailsValidator : AbstractValidator<Requests.ListNotificationsDetails>
{
    public ListNotificationsDetailsValidator()
    {
        RuleFor(request => request.Limit)
            .GreaterThan(0)
            .LessThanOrEqualTo(100);

        RuleFor(request => request.Offset)
            .GreaterThanOrEqualTo(0);
    }
}