using Contracts.DataTransferObjects;

namespace Contracts.Services.Account;

public static class Request
{
    public record DefineProfessionalAddress(Dto.Address Address);

    public record DefineResidenceAddress(Dto.Address Address);
}