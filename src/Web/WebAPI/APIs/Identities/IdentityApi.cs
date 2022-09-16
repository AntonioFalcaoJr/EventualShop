using Contracts.Services.Identity;
using WebAPI.Abstractions;

namespace WebAPI.APIs.Identities;

public static class IdentityApi
{
    public static void MapIdentityApi(this RouteGroupBuilder group)
    {
        group.MapGet("/login", ([AsParameters] Requests.Login request)
            => ApplicationApi.QueryAsync(request, (client, ct) => client.LoginAsync(request, cancellationToken: ct)));

        group.MapPost("/", ([AsParameters] Requests.RegisterUser request)
            => ApplicationApi.SendCommandAsync<Command.RegisterUser>(request));

        group.MapPost("/{userId:guid}/email", ([AsParameters] Requests.ChangeEmail request)
            => ApplicationApi.SendCommandAsync<Command.ChangeEmail>(request));

        group.WithMetadata(new TagsAttribute("IdentitiesV2"));
    }
}