namespace WebAPI.APIs.Shopping.Validators;

// public class AddShippingAddressValidator : AbstractValidator<Commands.AddShippingAddress>
// {
//     public AddShippingAddressValidator()
//     {
//         RuleFor(request => request.CartId)
//             .NotEmpty();
//
//         RuleFor(request => request.Address)
//             .SetValidator(new AddressValidator())
//             .OverridePropertyName(string.Empty);
//     }
// }