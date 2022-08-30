using WebAPI.Abstractions;

namespace WebAPI.APIs.Identities;

public static class IdentityApi
{
    public static void MapIdentityApi(this RouteGroupBuilder group)
    {
        group.MapGet("/login", ([AsParameters] Requests.Login request)
            => ApplicationApi.QueryAsync(request, (client, ct) => client.LoginAsync(request, cancellationToken: ct)));

        group.WithMetadata(new TagsAttribute("IdentitiesV2"));
    }
}