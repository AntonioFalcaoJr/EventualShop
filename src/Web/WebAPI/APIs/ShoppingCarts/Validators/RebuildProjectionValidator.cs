using FluentValidation;

namespace WebAPI.APIs.ShoppingCarts.Validators;

public class RebuildProjectionValidator : AbstractValidator<Requests.RebuildProjection>
{
    public RebuildProjectionValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty();
    }
}