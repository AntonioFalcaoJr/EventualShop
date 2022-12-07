using WebAPI.Abstractions;

namespace WebAPI.APIs.Communications;

public static class CommunicationApi
{
    public static RouteGroupBuilder MapCommunicationApi(this RouteGroupBuilder group)
    {
        group.MapGet("/", ([AsParameters] Requests.ListNotifications request)
            => ApplicationApi.QueryAsync(request, (client, cancellationToken) => client.ListEmailsAsync(request, cancellationToken: cancellationToken)));

        return group.WithMetadata(new TagsAttribute("Communications"));
    }
}