using Contracts.Services.Communication.Protobuf;
using WebAPI.Abstractions;
using WebAPI.APIs.Communications.Validators;

namespace WebAPI.APIs.Communications;

public static class Requests
{
    public record ListEmails(CommunicationService.CommunicationServiceClient Client, int? Limit, int? Offset, CancellationToken CancellationToken)
        : Validatable<ListEmailsValidator>, IQueryRequest<CommunicationService.CommunicationServiceClient>
    {
        public static implicit operator ListEmailsRequest(ListEmails request)
            => new()
            {
                Limit = request.Limit ?? default,
                Offset = request.Offset ?? default
            };
    }
}