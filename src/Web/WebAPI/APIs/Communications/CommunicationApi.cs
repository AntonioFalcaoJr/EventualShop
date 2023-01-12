using Asp.Versioning.Builder;
using Contracts.Services.Communication.Protobuf;
using WebAPI.Abstractions;

namespace WebAPI.APIs.Communications;

public static class CommunicationApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/communications/";

    public static IVersionedEndpointRouteBuilder MapCommunicationApiV1(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapGet("/notifications/details", ([AsParameters] Queries.ListNotificationsDetails query)
            => ApplicationApi.ListAsync<CommunicationService.CommunicationServiceClient, NotificationDetails>
                (query, (client, cancellationToken) => client.ListNotificationsDetailsAsync(query, cancellationToken: cancellationToken)));

        return builder;
    }

    public static IVersionedEndpointRouteBuilder MapCommunicationApiV2(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(2);

        group.MapGet("/notifications/details", ([AsParameters] Queries.ListNotificationsDetails query)
            => ApplicationApi.ListAsync<CommunicationService.CommunicationServiceClient, NotificationDetails>
                (query, (client, cancellationToken) => client.ListNotificationsDetailsAsync(query, cancellationToken: cancellationToken)));

        return builder;
    }
}