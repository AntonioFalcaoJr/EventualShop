using FluentValidation;

namespace WebAPI.APIs.Shopping.Validators;

public class GetShoppingCartDetailsValidator : AbstractValidator<Queries.GetShoppingCartDetails>
{
    public GetShoppingCartDetailsValidator()
    {
        RuleFor(request => request.CartId)
            .NotEmpty();
    }
}