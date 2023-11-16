using FluentValidation;

namespace Domain.Entities;

public class NotificationMethodValidator : AbstractValidator<NotificationMethod>
{
    public NotificationMethodValidator()
    {
        RuleFor(method => method.Id)
            .NotEmpty();
    }
}