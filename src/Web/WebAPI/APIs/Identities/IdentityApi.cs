using WebAPI.Abstractions;

namespace WebAPI.APIs.Identities;

public static class IdentityApi
{
    public static void MapIdentityApi(this RouteGroupBuilder group)
    {
        group.MapGet("/login", ([AsParameters] Requests.Login request)
            => ApplicationApi.QueryAsync(request, client => client.LoginAsync(request)));

        group.WithMetadata(new TagsAttribute("IdentitiesV2"));
    }
}