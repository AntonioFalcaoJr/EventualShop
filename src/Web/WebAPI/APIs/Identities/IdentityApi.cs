using Contracts.Services.Identity;
using WebAPI.Abstractions;

namespace WebAPI.APIs.Identities;

public static class IdentityApi
{
    public static RouteGroupBuilder MapIdentityApi(this RouteGroupBuilder group)
    {
        group.MapGet("/sign-in", ([AsParameters] Requests.SignIn request)
            => ApplicationApi.QueryAsync(request, (client, ct) => client.LoginAsync(request, cancellationToken: ct)));

        group.MapPost("/sign-up", ([AsParameters] Requests.SignUp request)
            => ApplicationApi.SendCommandAsync<Command.RegisterUser>(request));

        group.MapPut("/{userId:guid}/email", ([AsParameters] Requests.ChangeEmail request)
            => ApplicationApi.SendCommandAsync<Command.ChangeEmail>(request));

        return group.WithMetadata(new TagsAttribute("Identities"));
    }
}