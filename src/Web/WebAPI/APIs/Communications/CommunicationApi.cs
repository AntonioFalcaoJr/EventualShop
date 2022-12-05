using WebAPI.Abstractions;

namespace WebAPI.APIs.Communications;

public static class AccountApi
{
    public static RouteGroupBuilder MapCommunicationApi(this RouteGroupBuilder group)
    {
        group.MapGet("/", ([AsParameters] Requests.ListEmails request)
            => ApplicationApi.QueryAsync(request, (client, cancellationToken) => client.ListEmailsAsync(request, cancellationToken: cancellationToken)));

        return group.WithMetadata(new TagsAttribute("Communications"));
    }
}