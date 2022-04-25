using ECommerce.Contracts.Common;

namespace ECommerce.Contracts.Accounts;

public static class Request
{
    public record DefineProfessionalAddress(Models.Address Address);

    public record DefineResidenceAddress(Models.Address Address);
}