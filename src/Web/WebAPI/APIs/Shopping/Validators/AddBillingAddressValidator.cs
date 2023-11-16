namespace WebAPI.APIs.Shopping.Validators;

// public class AddBillingAddressValidator : AbstractValidator<Commands.AddBillingAddress>
// {
//     public AddBillingAddressValidator()
//     {
//         RuleFor(request => request.CartId)
//             .NotEmpty();
//
//         RuleFor(request => request.Address)
//             .SetValidator(new AddressValidator())
//             .OverridePropertyName(string.Empty);
//     }
// }