using Asp.Versioning.Builder;
using WebAPI.Abstractions;

namespace WebAPI.APIs.Communications;

public static class CommunicationApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/communications/";

    public static IVersionedEndpointRouteBuilder MapCommunicationApiV1(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapGet("/", ([AsParameters] Requests.ListNotifications request)
            => ApplicationApi.QueryAsync(request, (client, cancellationToken) => client.ListEmailsAsync(request, cancellationToken: cancellationToken)));

        return builder;
    }

    public static IVersionedEndpointRouteBuilder MapCommunicationApiV2(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(2);

        group.MapGet("/", ([AsParameters] Requests.ListNotifications request)
            => ApplicationApi.QueryAsync(request, (client, cancellationToken) => client.ListEmailsAsync(request, cancellationToken: cancellationToken)));

        return builder;
    }
}