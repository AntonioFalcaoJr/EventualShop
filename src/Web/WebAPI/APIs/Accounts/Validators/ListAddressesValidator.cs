using FluentValidation;

namespace WebAPI.APIs.Accounts.Validators;

public class ListAddressesValidator : AbstractValidator<Requests.ListAddresses>
{
    public ListAddressesValidator()
    {
        RuleFor(request => request.AccountId)
            .NotEmpty();
    }
}